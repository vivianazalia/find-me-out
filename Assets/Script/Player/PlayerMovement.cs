using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    PlayerSettings playerSettings;
    InputManager inputManager;
    CharacterController characterController;
    Transform cameraMain;
    Transform child;

    Vector3 verticalMove;
	
	public bool IsEnabled = true;

    void Awake()
    {
        playerSettings = GetComponent<PlayerSettings>();
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        child = transform.GetChild(0).transform;
    }

    private void Start()
    {
        cameraMain = Camera.main.transform;
    }
	
	void Update()
	{
		if(IsEnabled) Tick();
	}

    public void Tick()
    {
        handleHorizontalMovement();
        handleVerticalMovement();
        handleRotation();

        //Tiap 0.5 detik, instantiate Footstep di bawah
    }

    void handleRotation()
    {
        if (inputManager.IsMovePressed)
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(child.localEulerAngles.x, cameraMain.localEulerAngles.y, child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation, targetRotation, Time.deltaTime * playerSettings.rotationSpeed);
        }
    }

    void handleHorizontalMovement()
    {
        Vector3 move = (cameraMain.forward * inputManager.LeftStickInput.y + cameraMain.right * inputManager.LeftStickInput.x);
        move.y = 0f;
        if (inputManager.IsRunPressed)
        {
            characterController.Move(move * Time.deltaTime * playerSettings.runSpeed);
        }
        else
        {
            characterController.Move(move * Time.deltaTime * playerSettings.walkSpeed);
        }
    }

    void handleVerticalMovement()
    {
        if (characterController.isGrounded && verticalMove.y < 0)
        {
            verticalMove.y = 0f;
        }
        if (inputManager.IsJumpPressed && characterController.isGrounded)
        {
            verticalMove.y += Mathf.Sqrt(playerSettings.jumpHeight * -3.0f * playerSettings.gravityValue);
        }
        verticalMove.y += playerSettings.gravityValue * Time.deltaTime;
        characterController.Move(verticalMove * Time.deltaTime);
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float gravity = -0.1f;
            inputManager.AddGravity(gravity);
        }
        else
        {
            float gravity = playerSettings.gravityValue;
            inputManager.AddGravity(gravity);
        }
    }
}

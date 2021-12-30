using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    PlayerSettings playerSettings;
    InputManager inputManager;
    CharacterController characterController;
    Transform cameraMain;
    Transform child;

    CameraLook cam;

    [SerializeField] GameObject footstepPrefab;
    public int footstepLimit = 20;

    Vector3 verticalMove;

    public bool IsEnabled = true;

    void Awake()
    {
        playerSettings = GetComponent<PlayerSettings>();
        inputManager = GetComponent<InputManager>();
        characterController = GetComponent<CharacterController>();
        child = transform.GetChild(0).transform;
        cam = FindObjectOfType<CameraLook>();
    }

    private void Start()
    {
        if (hasAuthority)
        {
            cameraMain = Camera.main.transform;
            cam.SetPlayerTarget(this.transform);
        }
    }

    void Update()
    {
        if (IsEnabled && hasAuthority) Tick();
    }

    public void Tick()
    {
        handleHorizontalMovement();
        handleVerticalMovement();
        handleRotation();

        //Tiap 0.5 detik, instantiate Footstep di bawah

    }

    float timer = 0;
    int footstepSpawned = 0;
    void SpawnFootstep(float time)
    {
        if (footstepSpawned > footstepLimit)
        {
            return;
        }
        timer += Time.deltaTime;
        if (timer >= time && characterController.isGrounded)
        {
            // Instantiate object here
            Instantiate(footstepPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            // set timer to 0 to start countdown again
            timer = 0f;
            footstepSpawned++;
        }
    }

    void handleRotation()
    {
        if (cameraMain == null) return;
        if (inputManager.IsMovePressed)
        {
            Quaternion targetRotation = Quaternion.Euler(new Vector3(child.localEulerAngles.x, cameraMain.localEulerAngles.y, child.localEulerAngles.z));
            child.rotation = Quaternion.Lerp(child.rotation, targetRotation, Time.deltaTime * playerSettings.rotationSpeed);
        }
    }

    void handleHorizontalMovement()
    {
        if (cameraMain == null) return;
        Vector3 move = cameraMain.forward * inputManager.LeftStickInput.y + cameraMain.right * inputManager.LeftStickInput.x;
        move.y = 0f;
        if (inputManager.IsRunPressed)
        {
            characterController.Move(move * Time.deltaTime * playerSettings.runSpeed);
            //SpawnFootstep(0.2f);
        }
        else
        {
            characterController.Move(move * Time.deltaTime * playerSettings.walkSpeed);
            //SpawnFootstep(0.5f);
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

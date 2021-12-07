using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour //Per player 1. Todo: Pindah character controller ke parent, model 3d ke child
{
    CharacterController characterController;

    PlayerInput playerInput; //Auto generated

    PlayerSettings playerSetting;

    Vector2 leftStickInput;
    Vector2 rightStickInput;
    Vector3 leftStickVelocity;
    Vector3 rightStickVelocity;

    bool isMovePressed;
    bool isRunPressed;
    bool isJumpPressed;
    bool isLookPressed;

    bool isJumping;

    public bool IsMovePressed { get { return isMovePressed; } }
    public bool IsRunPressed { get { return isRunPressed; } }
    public bool IsJumpPressed { get { return isJumpPressed; } }
    public bool IsLookPressed { get { return isLookPressed; } }
    public Vector3 LeftStickVelocity { get { return leftStickVelocity; } }
    public void AddGravity(float gravityValue) { leftStickVelocity.y += gravityValue; }
    public Vector2 LeftStickInput { get { return leftStickInput; } }
    public Vector2 RightStickInput { get { return rightStickInput; } }
    public bool IsGrounded { get { return characterController.isGrounded; } }
    public bool IsJumping { get { return isJumping; } }

    public static event Action<Vector2> OnTouchStart;
    public static event Action<Vector2> OnTouchEnd;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        playerSetting = GetComponent<PlayerSettings>();

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;

        playerInput.CharacterControls.Look.started += OnLookInput;
        playerInput.CharacterControls.Look.canceled += OnLookInput;
        playerInput.CharacterControls.Look.performed += OnLookInput;

        playerInput.CharacterControls.Run.started += OnRunInput;
        playerInput.CharacterControls.Run.canceled += OnRunInput;

        playerInput.CharacterControls.Jump.started += OnJumpInput;
        playerInput.CharacterControls.Jump.canceled += OnJumpInput;

        playerInput.Touch.TouchPosition.started += OnTouchPositionStarted;
        playerInput.Touch.TouchPosition.canceled += OnTouchPositionEnd;

        playerInput.Touch.TouchPress.started += OnTouchInputStart;
        playerInput.Touch.TouchPress.canceled += OnTouchInputEnd;
    }

    private void OnTouchPositionEnd(InputAction.CallbackContext obj)
    {
        //throw new NotImplementedException();
    }

    private void OnTouchInputEnd(InputAction.CallbackContext context)
    {
        Debug.Log("Touch end");
        OnTouchEnd?.Invoke(playerInput.Touch.TouchPosition.ReadValue<Vector2>());
    }

    private void OnTouchInputStart(InputAction.CallbackContext context)
    {
        Debug.Log("Touch start");
        OnTouchStart?.Invoke(playerInput.Touch.TouchPosition.ReadValue<Vector2>());
    }

    private void OnTouchPositionStarted(InputAction.CallbackContext context)
    {
        //throw new NotImplementedException();
    }

    void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
        if (characterController.isGrounded)
        {
            leftStickVelocity.y += Mathf.Sqrt(playerSetting.jumpHeight * -3.0f * playerSetting.gravityValue);
            isJumping = true;
        }
    }

    void OnLookInput(InputAction.CallbackContext context)
    {
        rightStickInput = context.ReadValue<Vector2>(); //Taruh value dari context ke variabel
        rightStickVelocity.x = rightStickInput.x * playerSetting.lookSpeed; //Diproses
        rightStickVelocity.y = rightStickInput.y * playerSetting.lookSpeed;
        isLookPressed = rightStickInput.x != 0 || rightStickInput.y != 0;
    }

    void OnRunInput(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        leftStickInput = context.ReadValue<Vector2>(); //Taruh value dari context ke variabel
        if (isRunPressed)
        {
            leftStickVelocity.x = leftStickInput.x * playerSetting.runSpeed;
            leftStickVelocity.z = leftStickInput.y * playerSetting.runSpeed;
        }
        else
        {
            leftStickVelocity.x = leftStickInput.x * playerSetting.walkSpeed;
            leftStickVelocity.z = leftStickInput.y * playerSetting.walkSpeed;
        }
        isMovePressed = leftStickInput.x != 0 || leftStickInput.y != 0;
    }

    private void Update()
    {
        if (characterController.isGrounded && isJumping)
        {
            isJumping = false;
        }
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable(); //Harus di enable dan disable
        playerInput.Touch.Enable(); //Harus di enable dan disable
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
        playerInput.Touch.Disable();
    }
}

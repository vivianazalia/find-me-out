using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour //Warning! Avatar di animator gaada apa2nya
{
    CharacterController characterController;

    PlayerInput playerInput; //Auto generated

    Animator animator;
    int isWalkingHash;
    int isRunningHash;

    Vector2 currentMovementInput;
    Vector3 currentMovement;
    Vector3 currentRunMovement;

    [SerializeField] bool isMovementPressed;
    [SerializeField] bool isRunPressed;
    [SerializeField] bool isJumpPressed;
    [SerializeField] bool isWalking_;
    [SerializeField] bool isRunning_;
    [SerializeField] float rotationFactorPerFrame = 2.0f;
    [SerializeField] float walkSpeedMultiplier = 4.0f;
    [SerializeField] float runSpeedMultiplier = 8.0f;
    [SerializeField] float gravityFactor = -9.0f;
    [SerializeField] float jumpPower = 9.0f;

    private void Awake()
    {
        playerInput = new PlayerInput();
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");

        playerInput.CharacterControls.Move.started += OnMovementInput;
        playerInput.CharacterControls.Move.canceled += OnMovementInput;
        playerInput.CharacterControls.Move.performed += OnMovementInput;
        playerInput.CharacterControls.Run.started += OnRunInput;
        playerInput.CharacterControls.Run.canceled += OnRunInput;
    }

    void OnRunInput(InputAction.CallbackContext context)
    {
        isRunPressed = context.ReadValueAsButton();
    }

    void OnMovementInput(InputAction.CallbackContext context)
    {
        currentMovementInput = context.ReadValue<Vector2>(); //Taruh value dari context ke variabel
        //currentMovement.x = currentMovementInput.x * walkSpeedMultiplier; //Diproses
        currentMovement.x = 0;
        currentMovement.z = currentMovementInput.y * walkSpeedMultiplier;
        currentMovement = transform.TransformDirection(currentMovement); //Dijadikan local
        //currentRunMovement.x = currentMovementInput.x * runSpeedMultiplier;
        currentRunMovement.x = 0;
        currentRunMovement.z = currentMovementInput.y * runSpeedMultiplier;
        currentRunMovement = transform.TransformDirection(currentRunMovement);
        isMovementPressed = currentMovementInput.x != 0 || currentMovementInput.y != 0;
    }

    void handleRotation()
    {
        Quaternion currentRotation = transform.rotation; // ambil rotasi sekarang

        if (isMovementPressed)
        {
            if (currentMovementInput.x > 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(transform.right); //buat rotasi ke arah target
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime); //set rotasi
            }
            else if (currentMovementInput.x < 0)
            {
                Quaternion targetRotation = Quaternion.LookRotation(-transform.right); //buat rotasi ke arah target
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, rotationFactorPerFrame * Time.deltaTime); //set rotasi
            }
        }

    }

    void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        isWalking_ = isWalking;
        isRunning_ = isRunning;

        if(isMovementPressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (!isMovementPressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (isRunPressed && isMovementPressed && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        if (!isRunPressed && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
        if (isRunPressed && !isMovementPressed && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
    }

    void handleGravity()
    {
        if (characterController.isGrounded)
        {
            float gravity = -0.1f;
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
        else
        {
            float gravity = gravityFactor;
            currentMovement.y += gravity;
            currentRunMovement.y += gravity;
        }
    }

    // Update is called once per frame
    void Update()
    {
        handleGravity();
        handleRotation();
        handleAnimation();
        if (isRunPressed)
        {
            characterController.Move(currentRunMovement * Time.deltaTime);
        }
        else
        {
            characterController.Move(currentMovement * Time.deltaTime);
        }
    }

    private void OnEnable()
    {
        playerInput.CharacterControls.Enable(); //Harus di enable dan disable
    }

    private void OnDisable()
    {
        playerInput.CharacterControls.Disable();
    }
}

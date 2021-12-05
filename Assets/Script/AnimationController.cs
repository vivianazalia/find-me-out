using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimationController : MonoBehaviour //Kamera jangan lupa dilepas dari player
{
    InputManager inputManager;

    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        inputManager = GetComponent<InputManager>();

        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
    }
	
	private void Update()
	{
		handleAnimation();
	}
     
    public void handleAnimation()
    {
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isJumping = animator.GetBool(isJumpingHash);

        if(inputManager.IsMovePressed && !isWalking)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (!inputManager.IsMovePressed && isWalking)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (inputManager.IsRunPressed && inputManager.IsMovePressed && !isRunning)
        {
            animator.SetBool(isRunningHash, true);
        }
        if (!inputManager.IsRunPressed && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }
        if (inputManager.IsRunPressed && !inputManager.IsMovePressed && isRunning)
        {
            animator.SetBool(isRunningHash, false);
        }



        if (inputManager.IsJumping)
        {
            animator.SetBool(isJumpingHash, true);
            animator.SetBool(isRunningHash, false);
            animator.SetBool(isWalkingHash, false);
        }
        else
        {
            animator.SetBool(isJumpingHash, false);
        }
    }
}

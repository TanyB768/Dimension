using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator; // Reference varirable to Animator component
    //int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int doubleJumpHash;
    int isDashingHash;
    int isCrouchingHash;
    //bool isDoubleJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //Just to save some performance we use StringToHash()
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
        doubleJumpHash = Animator.StringToHash("doubleJump");
        isDashingHash = Animator.StringToHash("isDashing");
        isCrouchingHash = Animator.StringToHash("isCrouching");
    }

    // Update is called once per frame
    void Update()
    {
        //bool doubleJump = animator.GetBool(doubleJumpHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool forwardKey = Input.GetKey(KeyCode.RightArrow);
        bool jumpkey = Input.GetKey(KeyCode.Space);
        bool backwardKey = Input.GetKey(KeyCode.LeftArrow);
        if (!PauseMenu.gamePaused && !GameOver.gameOver && !LevelComplete.levelComplete)
        {
            // **** Idle to Running block Start ****
            if (!isRunning && forwardKey) 
            {
                //then make player run in forward
                animator.SetBool(isRunningHash, true);
            }
            if (!isRunning && backwardKey)
            {
                //then make player run backward
                animator.SetBool(isRunningHash, true);
            }
            if (isRunning && (!forwardKey && !backwardKey))
            {
                animator.SetBool(isRunningHash, false);
            }
            if (forwardKey && backwardKey)
            {
                animator.SetBool(isRunningHash, false);
            }
            // **** Idle to running block End ****

            // **** Idle to Jumping Block Start ****
            if (!isJumping && jumpkey)
            {
                animator.SetBool(isJumpingHash, true);
            }
            if (isJumping && Player.isGrounded)
            {
                animator.SetBool(isJumpingHash, false);
            }
            // For double Jump
            if (isJumping && Player.doubleJumpKey)
            {
                animator.SetBool(doubleJumpHash, true);
            }
            else
            {
                animator.SetBool(doubleJumpHash, false);
            }
            // Double Jump Block Ended

            // Dash Block Start
            if (isJumping && Player.isDashing)
            {
                animator.SetBool(isDashingHash, true);
            }
            else
            {
                animator.SetBool(isDashingHash, false);
            }
            // Dash Block End

            if (Player.isGrounded && jumpkey)
            {
                animator.SetBool(isJumpingHash, false);
            }
            // **** Idle to Jumping Block End ****

            // **** Running to Jumping Block Start ****
            if (isRunning && jumpkey)
            {
                animator.SetBool(isJumpingHash, true);
            }
            if (!isRunning && forwardKey)
            {
                animator.SetBool(isJumpingHash, false);
            }
            if (isJumping && !Player.isGrounded)
            {
                animator.SetBool(isJumpingHash, true);
            }
            if (Player.isGrounded && jumpkey && isRunning)
            {
                animator.SetBool(isJumpingHash, false);
            }
            // **** Running to Jumping Block End ****

            // When player is Freefalling
            if (!Player.isGrounded)
            {
                animator.SetBool(isJumpingHash, true);
            }
            
            // **** Idle to Crouching Block Start ****
            if(Player.isCrouching)
            {
                animator.SetBool(isCrouchingHash, true);
            }
            else
            {
                animator.SetBool(isCrouchingHash, false);
            }
            // **** Crouch Block End (also includes running to crouch and vice versa) ****
        
            // **** Slide Block Start
            if(Player.isSliding && !RayCast.onSlope)
            {
                animator.SetBool("isSliding", true);
            }
            else if(Player.isSliding && RayCast.onSlope)
            {
                animator.SetBool("isSliding", false);
            }
            else if(!Player.isSliding)
            {
                animator.SetBool("isSliding", false);
            }
            // **** Slide Block End
        }
    }
}
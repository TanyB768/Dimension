using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator animator; // Reference varirable to Animator component
    //int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //Just to save some performance we use StringToHash()
        //isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isJumpingHash = Animator.StringToHash("isJumping");
    }

    // Update is called once per frame
    void Update()
    {
        bool isRunning = animator.GetBool(isRunningHash);
        //bool isWalking = animator.GetBool(isWalkingHash);
        bool isJumping = animator.GetBool(isJumpingHash);
        bool forwardKey = Input.GetKey(KeyCode.RightArrow);
        //bool walkKey = Input.GetKey(KeyCode.RightControl);
        bool jumpkey = Input.GetKey(KeyCode.Space);
        bool backwardKey = Input.GetKey(KeyCode.LeftArrow);

        // **** Idle to Running block Start ****
        if (!isRunning && forwardKey) //&& Player.isGrounded)//if player presses rightArrow
        {
            //then make player run in forward
            animator.SetBool(isRunningHash, true);
        }
        if (!isRunning && backwardKey)// && Player.isGrounded)//if player presses leftArrow
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
        if(isJumping && !Player.isGrounded)
        {
            animator.SetBool(isJumpingHash, true);
        }
        if (Player.isGrounded && jumpkey && isRunning)
        {
            animator.SetBool(isJumpingHash, false);
        }
        // **** Running to Jumping Block End ****

        // When player is Freefalling
        if(!Player.isGrounded)
        {
            animator.SetBool(isJumpingHash, true);
        }
    }
}
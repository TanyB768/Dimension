using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    // Gameobjects as Audio Sources
    public GameObject footsteps;
    public GameObject grassLanding;
    public GameObject jump;
    public GameObject doubleJump;
    public GameObject slide;
    public GameObject dash;
    public GameObject crouchWalk;
    public GameObject wallClimb;
    
    // Audio Sources
    public AudioSource heart;
    public AudioSource powerUp;

    // Start is called before the first frame update
    void Start()
    {
        footsteps.SetActive(false);
        grassLanding.SetActive(false);
        jump.SetActive(false);
        doubleJump.SetActive(false);
        slide.SetActive(false);
        dash.SetActive(false);
        crouchWalk.SetActive(false);
        wallClimb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenu.gamePaused && !GameOver.gameOver && !LevelComplete.levelComplete)
        {
            // Running
            if (Player.isGrounded && Player.horizontalInput != 0)
                footsteps.SetActive(true);
            else
                footsteps.SetActive(false);

            // Double Jump
            if (Player.jumpKeyPressed)
                DoubleJumping();
            else
                NotDoubleJumping();

            // Single Jump
            if (Input.GetKey(KeyCode.Space) && Player.isGrounded)
                Jump();
            else
                NotJump();

            // Landing Sound
            if (Player.isGrounded)
            {
                grassLanding.SetActive(true);
                NotDoubleJumping();
            }
            else
                grassLanding.SetActive(false);

            // Slide 
            if (Player.isSliding)
            {
                slide.SetActive(true);
                footsteps.SetActive(false);
            }
            else
                slide.SetActive(false);

            // Dash
            if (Player.isDashing)
                dash.SetActive(true);
            else
                dash.SetActive(false);
            
            // Crouch Walk
            if (Player.isCrouching && Player.horizontalInput != 0)
            {
                footsteps.SetActive(false);
                crouchWalk.SetActive(true);
            }
            else
                crouchWalk.SetActive(false);

            if (Player.verticalInput != 0 && RayCast.onWall)
            {
                wallClimb.SetActive(true);
            }
            else
                wallClimb.SetActive(false);
        }
        else if (GameOver.gameOver || LevelComplete.levelComplete)
        {
            footsteps.SetActive(false);
            grassLanding.SetActive(false);
            jump.SetActive(false);
            doubleJump.SetActive(false);
            slide.SetActive(false);
            dash.SetActive(false);
            crouchWalk.SetActive(false);
            wallClimb.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11) // Heart layer for Heart Power Up
        {
            heart.Play();
        }
        if (other.gameObject.layer == 12) // Power Up Star for speed and invincibility
        {
            powerUp.Play();
        }
    }

    // These functions for jumps are created just to avoid confusion when triggering audio sources because
    // both jump and double jump are performed by the same button
    void Jump()
    {
        doubleJump.SetActive(true);
    }
    void NotJump()
    {
        doubleJump.SetActive(false);
    }
    void DoubleJumping()
    {
        jump.SetActive(true);
    }
    void NotDoubleJumping()
    {
        jump.SetActive(false);
    }
}
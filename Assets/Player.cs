using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform = null;//Exposing Transform as groundCheckTransfrom
    // to the inspector with null value
    [SerializeField] private LayerMask playerMask;
    public static bool isGrounded;
    private bool jumpKeyPressed;
    public static bool doubleJumpKey = false;
    public static bool isDashing = false;
    public static bool isGroundDashing = false;
    public static bool isCrouching = false;
    public static bool isSliding = false;
    public static bool isClimbingUp = false;
    public static bool isClimbingDown = false;
    public static bool onLedge = false;
    public float horizontalInput;// x-axis
    public static float verticalInput; // y-axis
    public static Rigidbody rigidbodyComponent;// to shorten the code by not writing getcomponent again
    [SerializeField] private float horizontalSpeed = 2.8f;// Original 2.7f
    [SerializeField] private float verticalSpeed = 2f;
    [SerializeField] private float crouchHeight = 0.25f;
    [SerializeField] private float standHeight = 0.5f;
    public static Vector3 moveDirection;
    public static int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        // Optimizes code & performance and doesn't need to fetch a component every single time.
        rigidbodyComponent = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    // General Rule: Check for key presses in update()
    void Update()
    {
        if (!PauseMenu.gamePaused && !GameOver.gameOver && !LevelComplete.levelComplete)
        {
            // Check is space key was pressed down
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpKeyPressed = true;
            }
            moveDirection = new Vector3(horizontalInput, 0f, 0f); // To get the direction of movement
            moveDirection.Normalize();
            verticalInput = Input.GetAxis("Vertical"); // To climb Walls ONLY
            horizontalInput = Input.GetAxisRaw("Horizontal");// No smoothing just raw input
            //horizontalInput = Input.GetAxis("Horizontal"); Smoothes out the player movement
            
            // To Prevent the player to shoot up when dashing towards sharp edges of level blocks
            if(rigidbodyComponent.velocity.y > 25f)
            {
                rigidbodyComponent.AddForce(Vector3.down * 10f, ForceMode.VelocityChange);
            }

            // To rotate the player
            if(moveDirection != Vector3.zero)// To check if the player is moving
            {
                // if it is, then rotate the player to it's move direction
                transform.right = moveDirection;
            }

            // For Crouching
            if (Input.GetKeyDown(KeyCode.DownArrow) && !isCrouching && isGrounded && !isSliding)
            {
                isCrouching = true;
                transform.localScale = new Vector3(standHeight, crouchHeight, standHeight);
                rigidbodyComponent.AddForce(Vector3.down * 50f, ForceMode.Impulse);
                horizontalSpeed = 1.25f;
            }
            if(Input.GetKeyUp(KeyCode.DownArrow) && isCrouching && isGrounded)
            {
                isCrouching = false;
                transform.localScale = new Vector3(standHeight, standHeight, standHeight);
                horizontalSpeed = 2.8f;
            }
            if(isCrouching && jumpKeyPressed)
            {
                isCrouching = false;
                transform.localScale = new Vector3(standHeight, standHeight, standHeight);
                horizontalSpeed = 2.8f;
            }

            // For Sliding
            if (Input.GetKeyDown(KeyCode.C) && !isSliding && isGrounded && !isCrouching && moveDirection != Vector3.zero && !RayCast.onSlope)
            {
                isSliding = true;
                Slide();
            }
            
            // For Double Jumps
            if (!isGrounded && !doubleJumpKey && Input.GetKeyDown(KeyCode.Space))
            {
                rigidbodyComponent.AddForce(Vector3.up * 4.5f, ForceMode.VelocityChange);
                doubleJumpKey = true;
            }
            
            // For Air Dash
            if (Input.GetKeyDown(KeyCode.RightControl) && !isGrounded && !isDashing)
            {
                rigidbodyComponent.AddForce(moveDirection.normalized * 100f, ForceMode.Impulse);
                isDashing = true;
            }
            else if (isDashing && !isGrounded)
                isDashing = true;
            else if (isGrounded)
                isDashing = false;

            // For Wall Climbing
            if (RayCast.onWall && !isGrounded)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    isClimbingUp = true;
                    rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, verticalInput * verticalSpeed, 0f);
                }
                else
                    isClimbingUp = false;
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    isClimbingDown = true;
                    rigidbodyComponent.velocity = new Vector3(rigidbodyComponent.velocity.x, verticalInput * verticalSpeed, 0f);
                    //transform.position += Vector3.up * Time.deltaTime * verticalSpeed;
                }
                else
                    isClimbingDown = false;
            }
            else
            {
                isClimbingUp = false;
                isClimbingDown = false;
            }
        }
    }
    // And apply those forces or actions in fixed update.
    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput * horizontalSpeed, rigidbodyComponent.velocity.y, 0f);//(x,y,z)
        if (Physics.OverlapSphere(groundCheckTransform.position,0.1f, playerMask).Length == 0)
        {
            //The overlapsphere() returns an array of colliders that it's collided with
            //the Length is the length of the array of colliders with which it has collided
            //so right now we have to check if it's colliding with anything at all so therefore,
            //Length == 0 checks the first position of the collider array
            //if it is 0 then it's not colliding with anything and we're in the air.
            isGrounded = false;
            return;
        }
        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length != 0)
            isGrounded = true;

        if (jumpKeyPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 6.4f, ForceMode.VelocityChange);//Original 6.35f
            jumpKeyPressed = false;
            doubleJumpKey = false;
        }
    }

    private void Slide()
    {
        transform.localScale = new Vector3(standHeight, crouchHeight, standHeight);
        rigidbodyComponent.AddForce(Vector3.down * 50f, ForceMode.Impulse);
        horizontalSpeed = 4f;
        Invoke("ResetSlide", 0.85f);// An Invoke() is called after a delay in seconds, here, 1 second.
        // Invoke() is used to reset slide after some time has passed.
    }
    private void ResetSlide() // To Reset Slide after 1 second
    {
        isSliding = false;
        transform.localScale = new Vector3(standHeight, standHeight, standHeight);
        horizontalSpeed = 2.8f;
    }

    private void OnTriggerEnter(Collider other) // For triggering coins & detecting ledges
    {
        if (other.gameObject.layer == 7) // Coin Layer
            Destroy(other.gameObject);

        if (other.gameObject.layer == 11) // Heart Layer
        {
            Debug.Log("Heart Pickup");
            if (HealthManager.health < 3)
            {
                HealthManager.health++;
                Destroy(other.gameObject);
            }
        }

        if(other.gameObject.layer == 12) // Power Up Star
        {
            Debug.Log("Power Up");
            Destroy(other.gameObject);
            StartCoroutine(PowerUpStar());
        }

        if (other.gameObject.layer == 8) // Ledge Layer (bool to play ledge climb animation) 
            onLedge = true;
    }
    private void OnTriggerExit(Collider other)
    {
        onLedge = false; // To stop/complete the ledge climb animation
    }

    private void OnCollisionEnter(Collision collision) // For Water Collision
    {
        if(collision.gameObject.CompareTag("WaterSurface"))
        {
            Debug.Log("Water Collision");
            isSliding = false;//To stop the player from increasing in size after gameover
            // Because the ResetSlide() is called 0.75 sec after Slide() is called.
            FindObjectOfType<GameOver>().PauseOnGameOver();
        }
        
        if(collision.gameObject.CompareTag("Spike")) // For Damaging and killing the player with spikes
        {
            //Debug.Log("Spike hit");
            isSliding = false;//To stop the player from increasing in size after gameover
            // Because the ResetSlide() is called 0.75 sec after Slide() is called.

            HealthManager.health--;
            if (HealthManager.health == 0)
            {
                FindObjectOfType<GameOver>().PauseOnGameOver();
            }
            else
                StartCoroutine(DamagePlayer());
        }

        if(collision.gameObject.CompareTag("LevelFinish")) // For Level Finish
        {
            Debug.Log("LevelFinish");
            FindObjectOfType<LevelComplete>().PauseOnLevelComplete();
        }
    }
    IEnumerator DamagePlayer()
    {
        Physics.IgnoreLayerCollision(9, 10); // To disable the collision between player and any damaging layer
        yield return new WaitForSeconds(1); // To wait for 1 Second
        Physics.IgnoreLayerCollision(9, 10, false); // To re-enable the collision between player and any damaging layer
    }

    IEnumerator PowerUpStar()
    {
        horizontalSpeed = 5;
        Physics.IgnoreLayerCollision(9, 10);
        yield return new WaitForSeconds(10);
        horizontalSpeed = 2.8f;
        Physics.IgnoreLayerCollision(9, 10, false);
    }
}
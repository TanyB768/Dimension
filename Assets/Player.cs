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
    public float horizontalInput;// x-axis
    private Rigidbody rigidbodyComponent;// to shorten the code by not writing getcomponent again
    [SerializeField] private float horizontalSpeed = 2.7f;
    private int direction = 1;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodyComponent = GetComponent<Rigidbody>();// Optimizes code & performance
        // and doesn't need to fetch a component every single time.
    }

    // Update is called once per frame
    // General Rule: Check for key presses in update()
    void Update()
    {   
        if(!PauseMenu.gamePaused)
        {
            // Check is space key was pressed down
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("Space Key was pressed");
                jumpKeyPressed = true;
            }
            horizontalInput = Input.GetAxisRaw("Horizontal");// No smoothing just raw input
            //horizontalInput = Input.GetAxis("Horizontal"); Smoothes out the player movement
            
            // To rotate the player
            if (Input.GetKey(KeyCode.LeftArrow) && direction == 1)
            {
                transform.Rotate(Vector3.up, 180.0f);
                direction = -1;
            }
            if (Input.GetKey(KeyCode.RightArrow) && direction != 1)
            {
                transform.Rotate(Vector3.up, 180.0f);
                direction = 1;
            }

            if (!isGrounded && !doubleJumpKey && Input.GetKeyDown(KeyCode.Space))
            {
                rigidbodyComponent.AddForce(Vector3.up * 4.5f, ForceMode.VelocityChange);
                doubleJumpKey = true;
            }
        }
    }
    // And apply those forces or actions in fixed update.
    private void FixedUpdate()
    {
        rigidbodyComponent.velocity = new Vector3(horizontalInput * horizontalSpeed, rigidbodyComponent.velocity.y, 0);//(x,y,z)
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
        {
            //The overlapsphere() returns an array of colliders that it's collided with
            //the Length is the length of the array of colliders with which it has collided
            //so right now we have to check if it's colliding with anything at all so therefore,
            //Length != 0 checks the first position of the collider array
            //if it is 0 then it's not colliding with anything and we're in the air.
            isGrounded = true;
        }
        if (jumpKeyPressed)
        {
            rigidbodyComponent.AddForce(Vector3.up * 6.35f, ForceMode.VelocityChange);
            jumpKeyPressed = false;
            doubleJumpKey = false;
        }
    }

    private void OnTriggerEnter(Collider other) // For triggering coins
    {
        if(other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision) // For Water Collision
    {
        if(collision.gameObject.CompareTag("WaterSurface"))
        {
            Debug.Log("Water Collision");
            FindObjectOfType<GameOver>().PauseOnGameOver();
        }

        if(collision.gameObject.CompareTag("LevelFinish")) // For Level Finish
        {
            Debug.Log("LevelFinish");
            FindObjectOfType<LevelComplete>().PauseOnLevelComplete();
        }
    }
}
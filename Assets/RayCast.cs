using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public float RayDistance = 5f;
    public static Vector3 groundNormal;
    public static bool onSlope = false;
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position + Vector3.down * 0.5f, Vector3.right * 4f);
        Debug.DrawRay(transform.position + Vector3.down * 0.5f, Vector3.left * 4f);
        Debug.DrawRay(transform.position, Vector3.down * 0.7f);
        RaycastHit hit;
        Ray rayRight = new Ray(transform.position + Vector3.down * 0.5f, Vector3.right * 4f);
        Ray rayLeft = new Ray(transform.position + Vector3.down * 0.5f, Vector3.left * 4f);
        Ray rayDown = new Ray(transform.position, Vector3.down * 0.7f);
        
        // Right Ray
        if(Physics.Raycast(rayRight, out hit, 4f))
        {
            if (hit.collider.tag == "Slope")
            {
                onSlope = true;
                Debug.Log("Slope");
                Debug.DrawRay(transform.position + Vector3.down * 0.5f, Vector3.right * 4f, Color.green);
                groundNormal = Vector3.ProjectOnPlane(transform.position, hit.normal);
                groundNormal.Normalize();
                Debug.DrawRay(transform.position, groundNormal, Color.red);
                if(Player.direction != 1)
                {
                    groundNormal = -Vector3.ProjectOnPlane(transform.position, hit.normal);
                    groundNormal.Normalize();
                    Debug.DrawRay(transform.position, groundNormal, Color.red);
                }
            }
            else
                onSlope = false;
        }
        
        // Left Ray
        if (Physics.Raycast(rayLeft, out hit, 4f))
        {
            if (hit.collider.tag == "Slope")
            {
                onSlope = true;
                Debug.Log("Slope");
                Debug.DrawRay(transform.position + Vector3.down * 0.5f, Vector3.left * 4f, Color.green);
                groundNormal = Vector3.ProjectOnPlane(transform.position, hit.normal);
                groundNormal.Normalize();
                Debug.DrawRay(transform.position, groundNormal, Color.red);
                if (Player.direction == 1)
                {
                    groundNormal = -Vector3.ProjectOnPlane(transform.position, hit.normal);
                    groundNormal.Normalize();
                    Debug.DrawRay(transform.position, groundNormal, Color.red);
                }
            }
            else
                onSlope = false;
        }

        // Ray Down to stop the player bouncing a little when landed on the ground
        if(Physics.Raycast(rayDown, out hit, 0.7f))//0.7f
        {
            if (hit.collider.tag == "Ground" && !Player.isGrounded)
            {
                Debug.Log("On Ground");
                Debug.DrawRay(transform.position, Vector3.down * 0.7f, Color.green);
                Player.rigidbodyComponent.AddForce(Vector3.down * 0.1f, ForceMode.VelocityChange);
            }
            if (hit.collider.tag == "WaterSurface")
            {
                Debug.Log("On Water");
                Debug.DrawRay(transform.position, Vector3.down * 0.7f, Color.green);
                Player.rigidbodyComponent.AddForce(Vector3.down * 0.1f, ForceMode.VelocityChange);
            }
        }
    }
}
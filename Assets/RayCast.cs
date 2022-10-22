using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCast : MonoBehaviour
{
    public float RayDistance = 5f;
    public static Vector3 groundNormal;
    public static bool onSlope = false;
    public static bool onWater = false;
    public static bool onWall = false;
    public static bool wallClimbAnim = false;
    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 0.7f);
        Debug.DrawRay(transform.position + Vector3.down * 0.35f, Player.moveDirection * 0.5f);
        Debug.DrawRay(transform.position + Vector3.up * 1.5f, Player.moveDirection * 0.5f);
        RaycastHit hit;
        Ray rayPlayerDirection = new Ray(transform.position + Vector3.down * 0.35f, Player.moveDirection * 0.5f);
        Ray rayDown = new Ray(transform.position, Vector3.down * 0.7f);

        // Ray for Player direction and climbing
        if(Physics.Raycast(rayPlayerDirection, out hit, 0.5f))
        {
            if(hit.collider.tag == "Wall")
            {
                onWall = true;
                Debug.DrawRay(transform.position + Vector3.down * 0.35f, Player.moveDirection * 0.5f, Color.green);
                groundNormal = Vector3.ProjectOnPlane(Player.moveDirection, hit.normal);
                groundNormal.Normalize();
            }
        }
        else
            onWall = false;
        
        // Ray Down to stop the player bouncing a little when landed on the ground
        if (Physics.Raycast(rayDown, out hit, 0.7f))//0.7f
        {
            if (hit.collider.tag == "Slope")
                onSlope = true;
            else
                onSlope = false;
        }
    }
}
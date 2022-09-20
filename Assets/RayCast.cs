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
        //Debug.DrawRay(transform.position, Vector3.down * 3f);
        RaycastHit hit;
        Ray rayRight = new Ray(transform.position + Vector3.down * 0.5f, Vector3.right * 4f);
        Ray rayLeft = new Ray(transform.position + Vector3.down * 0.5f, Vector3.left * 4f);
        //Ray rayDown = new Ray(transform.position, Vector3.down * 3f);

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
        /*
        // Down Ray
        if (Physics.Raycast(rayDown, out hit, 3f))
        {
            if (hit.collider.tag == "Slope")
            {
                onSlope = true;
                Debug.Log("Slope");
                Debug.DrawRay(transform.position, Vector3.down * 3f, Color.green);
            }
            else
            {
                onSlope = false;
            }
        }*/
    }
}
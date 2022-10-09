using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // This script adjusts the player model hieght while sliding and crouching to avoid abnormal player proportions
        if (Player.isSliding && !Player.isCrouching)
        {
            transform.localScale = new Vector3(1.5f, 2.35f, 1.6f);
        }
        else if(Player.isCrouching && !Player.isSliding)
        {
            transform.localScale = new Vector3(1.5f, 2.25f, 1.5f);
        }
        else
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        
    }
}

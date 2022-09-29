using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crouch : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Player.isCrouching)
        {
            transform.localScale = new Vector3(1.5f, 2.25f, 1.5f);
        }
        else
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }
}

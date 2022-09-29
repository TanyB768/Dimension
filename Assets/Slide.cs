using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Player.isSliding)
        {
            transform.localScale = new Vector3(1.5f, 2.35f, 1.6f);
        }
        else
        {
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
    }
}

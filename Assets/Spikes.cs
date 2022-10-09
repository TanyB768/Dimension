using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spikes : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<GameOver>().PauseOnGameOver();
    }
}

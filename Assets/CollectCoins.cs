//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class CollectCoins : MonoBehaviour
{
    public AudioSource collectSound;
    public static bool updateScore = false;

    void OnTriggerEnter(Collider other)
    {
        collectSound.Play();
        ScoreSystem.theScore += 50;
        updateScore = true;
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health = 3; // No. of max hearts

    public Image[] hearts; // Array of type Image called hearts
    public Sprite fullHeart; // Stores full heart image
    public Sprite emptyHeart; // Stores empty heart image
    //Sprites are 2D graphic objects used for characters, props, projectiles and other elments of 2D gameplay or UI
    void Awake() // Awake is used to replenish hearts when the game is over and restarts
    {
        health = 3; // Because static datatypes persists between Scene changes.
    }
    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts) // For each Image "img"(var name) in hearts[] array
        {
            img.sprite = emptyHeart;// Fills img.sprite with empty hearts image
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart; // Fills img.sprite with full hearts image
        }
    }
}

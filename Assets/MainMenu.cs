using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//Whenever we want to deal with different scenes

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // SceneManager.LoadScene("Level 01"); we can also give scene name
        // But for now we're loading the scene which is next in the queue. Below we are getting
        // the index of our currently loaded/active scene.
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("Game");
        ScoreSystem.theScore = 0;
        Debug.Log("Playing Game");
    }

    public void QuitGame()
    {
        Debug.Log("Quit !");
        Application.Quit();
    }
}

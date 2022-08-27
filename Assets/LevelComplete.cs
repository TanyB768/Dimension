using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public static bool levelComplete = false;
    public GameObject levelCompleteUI;
    private void Start()
    {
        levelCompleteUI.SetActive(false);
    }

    public void Replay()
    {
        levelCompleteUI.SetActive(false);
        Time.timeScale = 1f;
        levelComplete = false;
        SceneManager.LoadScene("Game");
        ScoreSystem.theScore = 0;
    }
    public void PauseOnLevelComplete()
    {
        levelCompleteUI.SetActive(true);
        Time.timeScale = 0f;
        levelComplete = true;
    }

    public void MainMenu()
    {
        Debug.Log("Lodaing Menu");
        levelComplete = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
        ScoreSystem.theScore = 0;
    }

    public void QuitGame()
    {
        Debug.Log("Quitting Game");
        Application.Quit();
    }
}
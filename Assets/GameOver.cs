using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static bool gameOver = false;
    public GameObject gameOverUI;
    private void Start()
    {
        gameOverUI.SetActive(false);
    }

    public void Replay()
    {
        gameOverUI.SetActive(false);
        Time.timeScale = 1f;
        gameOver = false;
        SceneManager.LoadScene("Game");
        ScoreSystem.theScore = 0;
    }
    public void PauseOnGameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
        gameOver = true;
    }

    public void MainMenu()
    {
        Debug.Log("Lodaing Menu");
        gameOver = false;
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
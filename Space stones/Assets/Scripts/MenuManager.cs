using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Text bestScoreText;

    public void Update()
    {
        BestScoreUpdate();
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("level", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void DeleteBestScore()
    {
        PlayerPrefs.DeleteKey("bestScore");
    }

    public void BestScoreUpdate()
    {
        bestScoreText.text = $"BEST SCORE: {PlayerPrefs.GetInt("bestScore", 0)}";
    }

    public void Exit()
    {
        Application.Quit();
    }
}

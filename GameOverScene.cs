using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverScene : MonoBehaviour
{

    public string game;
    public string mainMenu;
    public TextMeshProUGUI highScoreGUI;
    public TextMeshProUGUI currentScoreGUI;

    private int highScore;
    private int currentScore;
    void Start()
    {
        currentScore = PlayerPrefs.GetInt("currentScore", 0);
        currentScoreGUI.text = currentScore.ToString();

        highScore = PlayerPrefs.GetInt("highScore", 0);
        highScoreGUI.text = highScore.ToString();
    }
    void Update()
    {

    }

    public void StartGame()
    {
        SceneManager.LoadScene(game);
    }


    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    Monkey monkey;

    public string gameOver;
    void Start()
    {
        monkey = GameObject.FindGameObjectWithTag("Monkey").GetComponent<Monkey>();
    }
    void Update()
    {
        if (monkey.lifes == 0)
        {
            if (monkey.score > PlayerPrefs.GetInt("highScore", 0))
            {
                PlayerPrefs.SetInt("highScore", monkey.score);
            }
            PlayerPrefs.SetInt("currentScore", monkey.score);
            
            SceneManager.LoadScene(gameOver);
        }
    }

}

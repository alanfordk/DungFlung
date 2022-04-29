using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public string game;
    public string options;
    public TextMeshProUGUI scoreGUI;
    public AudioManager audioManager;
    public int score;
    public int previousSoundEffectSetting;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        previousSoundEffectSetting = PlayerPrefs.GetInt("soundEffectsSetting");
    }
    void Start()
    {
        score = PlayerPrefs.GetInt("highScore", 0);
        scoreGUI.text = score.ToString();

        if(previousSoundEffectSetting == 0)
        {
            audioManager.soundEffects = false;
        }
        else
        {
            audioManager.soundEffects = true;
        }

    }
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(game);
    }

    public void OpenOptions()
    {
        SceneManager.LoadScene(options);
    }

    public void CloseOptions()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}

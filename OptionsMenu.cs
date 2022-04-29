using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    public string mainMenu;

    public AudioManager audioManager;
    public Toggle soundEffectsToggle;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        soundEffectsToggle = GameObject.FindGameObjectWithTag("soundEffectsToggle").GetComponent<Toggle>();
        if (audioManager.soundEffects)
        {
            soundEffectsToggle.isOn = true;
        }
        if (!audioManager.soundEffects)
        {
            soundEffectsToggle.isOn = false;
        }

    }
    void Start()
    {
        soundEffectsToggle.onValueChanged.AddListener(delegate
        {
            soundEffectsChanged();
        });
    }

    void Update()
    {

    }

    public void Back()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void ResetHighScore()
    {
        //ResetHighScore
        PlayerPrefs.DeleteKey("highScore");
    }


    public void soundEffectsChanged()
    {
        if (audioManager.soundEffects)
        {
            soundEffectsToggle.isOn = false;
            audioManager.soundEffects = false;
            PlayerPrefs.SetInt("soundEffectsSetting", 0);
        }
        else
        {
            soundEffectsToggle.isOn = true;
            audioManager.soundEffects = true;
            PlayerPrefs.SetInt("soundEffectsSetting", 1);
        }
    }
}

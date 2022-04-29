using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    private TextMeshProUGUI scoreCounterTxt;
    public int scoreValue;

    private void Awake()
    {
        scoreCounterTxt = GetComponent<TextMeshProUGUI>();
    }
    void Start()
    {
        scoreValue = 0;
    }

    void Update()
    {
        scoreCounterTxt.text = scoreValue.ToString();
    }

    void scoreUpdate()
    {

    }
}

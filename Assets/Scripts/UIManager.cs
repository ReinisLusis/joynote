using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public Text countdownText;
    public int initialLives;
    private int score;
    private int lives;
    private float countdownTime;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = string.Empty;
        score = 0;
        lives = initialLives;
        countdownTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = string.Format("Score: {0}", score);
        livesText.text = string.Format("Lives: {0}/{1}", lives, initialLives);

        if(countdownTime < -0.01)
        {
            countdownText.text = Math.Abs(countdownTime).ToString("0.0");
        } 
    }

    public void UpdateCountDown(float seconds)
    {
        countdownTime = seconds;
        if(seconds > -0.1)
        {
            countdownText.enabled = false;
        }
    }

    internal void UpdateScore(bool v)
    {
        if (v) score++;
        else lives--;
    }
}

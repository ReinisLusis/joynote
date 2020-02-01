using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public Text livesText;
    public int initialLives;
    private int score;
    private int lives;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = string.Empty;
        score = 0;
        lives = initialLives;
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = string.Format("Score: {0}", score);
        livesText.text = string.Format("Lives: {0}/{1}", lives, initialLives);
    }

    internal void UpdateScore(bool v)
    {
        if (v) score++;
        else lives--;
    }
}

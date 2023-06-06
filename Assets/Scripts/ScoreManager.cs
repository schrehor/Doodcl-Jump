using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    
    public Text scoreText;

    //public float TopScore { get; private set; }
    public float GameScore { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        scoreText.text = Mathf.Round(GameScore).ToString(CultureInfo.InvariantCulture);
    }
    
    void Update()
    {
        if (transform.position.y > GameScore)
        {
            GameScore = transform.position.y;
        }
        
        scoreText.text = Mathf.Round(GameScore).ToString(CultureInfo.InvariantCulture);;
    }
    
    // public void UpdateTopScore()
    // {
    //     if (GameScore > TopScore)
    //     {
    //         TopScore = GameScore;
    //     }
    // }
}

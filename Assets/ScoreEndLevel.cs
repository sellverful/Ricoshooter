using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ScoreEndLevel : MonoBehaviour {

    public PlayerController player;
    public Text score;
    public Text scoreRank;
    public Text timeScoreText;
    public Text rankScoreT;
    public Text rankTimeT;
    // public Text lifes;


    private String rankScore = "D";
    private String rankTime = "SSS";
    private String rankFinal = "D";
    private int scoreLife = 1000;
    private float checkerS = 0;
    private float checkerT = 5;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }

    void FixedUpdate()
    {
     //   ExtraLife();
        CheckRank(player.score);
        CheckRankTime(player.timeScore);
        CalculateFinalRank();
        score.text = player.score.ToString("0");
        scoreRank.text = rankFinal;
        rankScoreT.text = rankScore;
        rankTimeT.text = rankTime;  
        timeScoreText.text = (player.timeScore).ToString("0");
        // lifes.text = "Lifes: " + player.curHealth;
    }

    private void CalculateFinalRank()
    {
        switch ((int)(checkerT + checkerS) / 2)
        {
            case 1:
                rankFinal = "D";
                break;
            case 2:
                rankFinal = "C";
                break;
            case 3:
                rankFinal = "B";
                break;
            case 4:
                rankFinal = "A";
                break;
            case 5:
                rankFinal = "SSS";
                break;
        }
    }
    private void CheckRank(float x)
    {

        switch ((int)x)
        {
            case 2000:
                rankScore = "D";
                checkerS = 1;
                break;
            case 2500:
                rankScore = "C";
                checkerS = 2;
                break;
            case 3000:
                rankScore = "B";
                checkerS = 3;
                break;
            case 4500:
                rankScore = "A";
                checkerS = 4;
                break;
            case 5000:
                rankScore = "SSS";
                checkerS = 5;
                break;
        }
    }
    private void CheckRankTime(float x)
    {
        switch ((int)x)
        {
            case 500:
                rankTime = "SSS";
                checkerT = 5;
                break;
            case 1000:
                rankTime = "A";
                checkerT = 4;
                break;
            case 3000:
                rankTime = "B";
                checkerT = 3;
                break;
            case 4500:
                rankTime = "C";
                checkerT = 2;
                break;
            case 5000:
                rankTime = "D";
                checkerT = 1;
                break;
        }
    }

    public void ExtraLife()
    {
        if (player.score == scoreLife)
        {
            player.curHealth += 1;
            scoreLife += 1000;
        }
    }
}

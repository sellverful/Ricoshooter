using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class ScoreEndLevel : MonoBehaviour {

    public PlayerController player;
    public TextMeshProUGUI score;
    public TextMeshProUGUI scoreRank;
    public TextMeshProUGUI timeScoreText;
    public TextMeshProUGUI rankScoreT;
    public TextMeshProUGUI rankTimeT;
    // public Text lifes;


    private String rankScore = "D";
    private String rankTime = "SSS";
    private String rankFinal = "D";
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
        CheckRank(player.score);
        CheckRankTime(player.timeScore);
        CalculateFinalRank();
        score.text = player.score.ToString("0");
        scoreRank.text = rankFinal;
        rankScoreT.text = rankScore;
        rankTimeT.text = rankTime;  
        timeScoreText.text =  String.Format("{0:0}:{1:00}", Mathf.Floor(player.timeScore / 60), player.timeScore % 60); ;
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

        if (x >= 500) { 
            rankScore = "D";
        checkerS = 1;
        }
        if (x >= 1000)
        {
            rankScore = "C";
            checkerS = 2;
        }
        if (x >= 1500)
        {
            rankScore = "B";
            checkerS = 3;
        }
        if (x >= 2000)
        {
            rankScore = "A";
            checkerS = 4;
        }
        if (x >= 2500)
        {
            rankScore = "SSS";
            checkerS = 5;
        }
        
    }
    private void CheckRankTime(float x)
    {
        Debug.Log(rankTime);
        if (x >= 60)
        {
            rankTime = "A";
            checkerT = 4;
        }
        if (x >= 90)
        {
            rankTime = "B";
            checkerT = 3;
        }
        if (x >= 120)
        {
            rankTime = "C";
            checkerT = 2;
        }
        if (x >= 150)
        {
            rankTime = "D";
            checkerT = 1;
        }
        }
    
}

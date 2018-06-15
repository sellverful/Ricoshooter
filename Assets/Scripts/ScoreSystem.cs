using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class ScoreSystem : MonoBehaviour
{

    public PlayerController player;
    public TextMeshProUGUI score;
    public TextMeshProUGUI scoreRank;
   // public Text lifes;

    private float timeScore = 0;
    private String rank = "";
    private int scoreLife = 5000;

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }
    }

    void FixedUpdate()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        ExtraLife();
        CheckRank(player.score);
        score.text = player.score.ToString("0");
        scoreRank.text = rank;
       // lifes.text = "Lifes: " + player.curHealth;
    }

    private void CheckRank(float x)
    {
        if (x >= 500)
        {
            rank = "D";
        }
        if (x >= 1000)
        {
            rank = "C";
        }
        if (x >= 1500)
        {
            rank = "B";
        }
        if (x >= 2000)
        {
            rank = "A";
        }
        if (x >= 2500)
        {
            rank  = "SSS";
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


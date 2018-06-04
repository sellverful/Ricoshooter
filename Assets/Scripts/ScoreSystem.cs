using System;
using UnityEngine;
using UnityEngine.UI;


public class ScoreSystem : MonoBehaviour
{

    public PlayerController player;
    public Text score;
    public Text scoreRank;
   // public Text lifes;

    private float timeScore = 0;
    private String rank = "";
    private int scoreLife = 1000;

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
       //scoreRank.text = rank;
       // lifes.text = "Lifes: " + player.curHealth;
    }

    private void CheckRank(float x)
    {
        switch ((int)x)
        {
            case 200:
                rank = "D";
                break;
            case 400:
                rank = "C";
                break;
            case 600:
                rank = "B";
                break;
            case 800:
                rank = "A";
                break;
            case 1000:
                rank = "SSS";
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


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public GameObject levelchoose;
    public GameObject dlc;
    public void Start()
    {
        levelchoose.SetActive(false);
        dlc.SetActive(false);
    }
    public void newGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void quitGame(){
		Debug.Log ("Quit");
		Application.Quit();
	}
    public void Credits()
    {
        SceneManager.LoadScene("Titles");
    }
	public void shortcut(){
		SceneManager.LoadScene("EnemyTest");
	}
    public void levels()
    {
        gameObject.SetActive(false);
        levelchoose.SetActive(true);
    }
    public void levels_dlc()
    {
        gameObject.SetActive(false);
        dlc.SetActive(true);
    }
}

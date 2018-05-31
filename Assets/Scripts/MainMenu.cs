using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
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
}

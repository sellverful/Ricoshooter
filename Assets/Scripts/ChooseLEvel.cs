using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseLEvel : MonoBehaviour {
    public GameObject mainmenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void main_menu()
    {
        mainmenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void tutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void level_1()
    {
        SceneManager.LoadScene("FirstLevel");
    }
    public void level_2()
    {
        SceneManager.LoadScene("SecondLevel");
    }
    public void level_3()
    {
        SceneManager.LoadScene("LastTrial");
    }
    
}

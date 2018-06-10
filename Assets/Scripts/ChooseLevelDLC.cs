using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChooseLevelDLC : MonoBehaviour {
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
        SceneManager.LoadScene("NewLevel");
    }
    public void level_2()
    {
        //SceneManager.LoadScene("SecondLevel");
    }
}

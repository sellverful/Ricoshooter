using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinOrLose : MonoBehaviour {
    public Text win;
    public Text lose;
    public LeftCursorHandler lch;
    public Canvas minigame;
	// Use this for initialization
	void Start () {
        win.enabled = false;
        lose.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(lch.countwin == 3)
        {
            Time.timeScale = 0.0f;
            win.enabled = true;
            minigame.enabled = false;
        }
        else if (lch.countlose == 3)
        {
            Time.timeScale = 0.0f;
            lose.enabled = true;
            minigame.enabled = false;
        }
        if (Input.GetKeyUp(KeyCode.Escape) && Time.timeScale == 0.0f)
        {
            SceneManager.LoadScene("Hacc2");
        }
	}
}

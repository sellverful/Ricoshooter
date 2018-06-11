using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinOrLose : MonoBehaviour {
    public Text win;
    public Text lose;
    public LeftCursorHandler lch;
    public RightCursorHandler rch;
    public GameObject minigame;
    public PlayerController player;
    // Use this for initialization
    void Start () {
        win.enabled = false;
        lose.enabled = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }
	
	// Update is called once per frame
	void Update () {
		if(lch.countwin == 3)
        {
            Time.timeScale = 0.0f;
            win.enabled = true;
            minigame.SetActive(false);
            lch.gameObject.SetActive(false);
            rch.gameObject.SetActive(false);
            StartCoroutine(wait());
            win.enabled = false;
            player.enabled = true;
            Time.timeScale = 1.0f;
        }
        else if (lch.countlose == 3)
        {
            Time.timeScale = 0.0f;
            lose.enabled = true;
            minigame.SetActive(false);
            lch.gameObject.SetActive(false);
            rch.gameObject.SetActive(false);
            StartCoroutine(wait());
            lose.enabled = false;
            player.enabled = true;
            Time.timeScale = 1.0f;
        }
        /*if (Input.GetKeyUp(KeyCode.Escape) && Time.timeScale == 0.0f)
        {
            SceneManager.LoadScene("Hacc2");
        }*/
	}
    IEnumerator wait()
    {
        yield return new WaitForSeconds(10);
    }
}

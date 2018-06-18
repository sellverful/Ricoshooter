using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipUI : MonoBehaviour {
    
    public GameObject player;
    private GameObject skip;
	// Use this for initialization
	void Start () {
        skip = GameObject.FindGameObjectWithTag("SkipUI");
        player = GameObject.FindGameObjectWithTag("Player").transform.Find("Gun").gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = 0f;
        player.SetActive(false);
    }
    public void Yes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void No()
    {
        Time.timeScale = 1f;
        player.SetActive(true);
        Destroy(skip);
    }
}

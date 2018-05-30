using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartLevel : MonoBehaviour {
	PlayerController player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	public void Restart(){
			player.dead = false;
			string scene = SceneManager.GetActiveScene ().name;
			SceneManager.LoadScene (scene);
			Time.timeScale = 1f;
	}
}
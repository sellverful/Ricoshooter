using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResurrectionPoint : MonoBehaviour {
	PlayerController player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (player.dead) {
			//player.transform.position = transform.position;
			player.dead = false;
			string scene =  SceneManager.GetActiveScene().name;
			SceneManager.LoadScene (scene);
		}
	}
}

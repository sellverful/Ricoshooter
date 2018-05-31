using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemies : MonoBehaviour {
	public Doors doors;
	public int enemiesAmount;
	public int count;
	public bool isHere;
	// Use this for initialization
	void Start () {
		enemiesAmount = transform.childCount;
		for (int i = 0; i < enemiesAmount; i++) {
			transform.GetChild (i).gameObject.SetActive (false);
		}
	}
	
	void OnTriggerEnter(Collider col){
		
		if (col.tag == "Player") {
			for (int i = 0; i < enemiesAmount; i++) {
				transform.GetChild (i).gameObject.SetActive (true);
			}
		}
	}
	void Update(){
		if (enemiesAmount==count) {
			if(doors!=null)
				doors.doorsActive = true;
			Destroy (gameObject, 2);
		}
	}

}

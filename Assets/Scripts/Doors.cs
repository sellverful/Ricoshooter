using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour {
	private Animator anim;
	bool doorOpen;
	public bool doorsActive = false;
	private bool once = false;
	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		doorOpen = false;
	}
	
	void OnTriggerEnter(Collider col){
		if (col.tag == "Player" && doorsActive) {
			doorOpen = true;
			DoorsAnim ("Open");
		}
	}
	void OnTriggerStay(Collider col){
		if (col.tag == "Player" && doorsActive && !once) {
			doorOpen = true;
			DoorsAnim ("Open");
			once = !once;
		}
	}
	void OnTriggerExit(Collider col){
		if (doorOpen && col.tag == "Player") {
			doorOpen = false;
			DoorsAnim ("Close");
		}
	}

	void DoorsAnim(string direction){
		anim.SetTrigger(direction);
	}
}

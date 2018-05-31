using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerablePoint : MonoBehaviour {
	public bool destroyed = false;
	public Transform particle1;
	public Transform particle2;
	void OnEnable(){
		particle1.gameObject.SetActive (true);
	}
	void OnDisable(){
		particle1.gameObject.SetActive (false);
	}
	void OnTriggerEnter(Collider col){
		if (!enabled || destroyed) return;
		if (col.tag == "Bullet" && col.GetComponent<Bullet> () != null) {
			Bullet bul = col.GetComponent<Bullet> ();
			if (bul.playersBullet) {
				GetComponent<AudioSource> ().Play ();
				destroyed = true;
				particle1.gameObject.SetActive (false);
				particle2.gameObject.SetActive (true);
				Destroy (col.gameObject);
			}
		}
	}
}

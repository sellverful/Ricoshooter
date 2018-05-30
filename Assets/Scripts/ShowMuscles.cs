using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShowMuscles : MonoBehaviour {
	private Boss boss;
	public GameObject bossG;
	public GameObject spinner;
	public GameObject guns;
	public GameObject shields;
	private Animator spinnerAnimator;
	private bool goUpBaby = false;
	private bool once = false;
	private AudioSource au;
	public AudioClip hahaha;
	// Use this for initialization
	void Start () {
		boss = GetComponent<Boss> ();
		spinnerAnimator = spinner.GetComponent<Animator> ();
		spinner.SetActive (false);
		guns.SetActive (false);
		shields.SetActive (false);
		au = GetComponent<AudioSource> ();
		//StartCoroutine (Show ());
	}
	void OnTriggerEnter (Collider col){
		if (!enabled)
			return;
		if (col.tag == "Bullet") {
			if(col.GetComponent<Bullet>()!=null)
				Destroy (col.gameObject);
		}
		if (col.tag == "Bullet" && !once) {
			once = true;
			StartCoroutine (Show ());
		}
	}
	// Update is called once per frame
	void Update () {
		if (goUpBaby) {
			Vector3 goUp = new Vector3 (transform.position.x, 1.2f, transform.position.z);
			transform.position = Vector3.MoveTowards (transform.position,goUp,Time.deltaTime);
		}

	}

	IEnumerator Show(){
		au.PlayOneShot (hahaha);
		yield return new WaitForSeconds (4f);
		goUpBaby = true;
		spinner.SetActive (true);
		yield return new WaitForSeconds (2f);
		goUpBaby = false;
		spinnerAnimator.SetTrigger ("ShowMuscles");
		yield return new WaitForSeconds (0.5f);
		guns.SetActive (true);
		yield return new WaitForSeconds (2f);
		shields.SetActive (true);
		yield return new WaitForSeconds (2f);
		boss.enabled = true;
		GetComponent<Wandering> ().enabled = true;
		GetComponent<NavMeshAgent> ().enabled = true;
		enabled = false;
	}
}

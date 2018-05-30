using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy1 : MonoBehaviour {
	public Transform player;
	NavMeshAgent agent;
	// Use this for initialization
	public float hitRate = 1f;
	public AudioClip hitSound;
	public AudioClip dieSound;
	private AudioSource audioS;
	float time;
	private bool dead = false;
	private Animator anim;
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
		player = GameObject.FindGameObjectWithTag ("Player").transform;
		anim = GetComponent<Animator> ();
		audioS = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead){
			time -= Time.deltaTime;
			if (Vector3.Distance (player.position, this.transform.position) > 2f) {
				anim.SetTrigger ("Chase");
				agent.SetDestination (player.position);
			} else if (Vector3.Distance (player.position, this.transform.position) < 2f && time < 0) {
				
				DealDamage ();
				time = hitRate;
			} 
		}

	}
	void DealDamage(){
		anim.SetTrigger ("Hit");
		audioS.PlayOneShot (hitSound);
		StartCoroutine (Hit());
	}
	IEnumerator Hit(){
		yield return new WaitForSeconds (0.5f);
		player.SendMessage ("DealDamage", 1f);
	}
	void OnDestroy(){
		if(gameObject.GetComponentInParent<ActivateEnemies> ()!=null)
			gameObject.GetComponentInParent<ActivateEnemies> ().count++;
	}
	void Die(){
		dead = true;
		agent.Stop ();
		anim.SetTrigger ("Die");
		GetComponent<BoxCollider> ().enabled = false;
		//audioS.volume = 0.7f;
		if(!audioS.isPlaying)
			audioS.PlayOneShot (dieSound);
		Destroy (gameObject, 1.2f);
	}
}

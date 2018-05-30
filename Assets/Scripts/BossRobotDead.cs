using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRobotDead : MonoBehaviour {
	private Boss boss;
	private SimpleLookAt look;
	private Animator anim;
	private bool once = false;
	// Use this for initialization
	void Start () {
		boss = GameObject.FindGameObjectWithTag ("Boss").GetComponent<Boss> ();
		look = GetComponent<SimpleLookAt> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (boss.dead && !once) {
			look.enabled = false;
			anim.SetTrigger ("Dead");
			transform.localPosition = new Vector3 (transform.localPosition.x,-0.179f,transform.localPosition.z);
			//-0.0225
			once = true;
		}
	}
}

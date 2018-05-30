using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceSystem : MonoBehaviour {
	public EnemyBullet bullet;
	public float bulletSpeed; 
	public float deathTimerBullet;
	// Use this for initialization
	private int j = 0;

	void Start () {
		InvokeRepeating ("DefenceSystemActivate",1f,1.5f);

	}
	
	// Update is called once per frame
	void Update () {
		//InvokeRepeating ("DefenceSystemActivate",1f,1f);
	}
	void DefenceSystemActivate(){
		
		for (int i = 0; i < 360; i+=36) {
			EnemyBullet bul =  Instantiate(bullet,transform.position,transform.rotation*Quaternion.Euler(0,i+j,0));
			bul.speed = bulletSpeed;
			bul.deathTimer = deathTimerBullet;
		}
		j += 15;
	}


}

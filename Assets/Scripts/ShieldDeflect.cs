using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeflect : MonoBehaviour {

	public int health = 3;
	private Boss boss;
	private Renderer rend;
	private Color previousColor;
	private int startinghealth;
	private bool once = false;
	void Start(){
		startinghealth = health;
		rend = GetComponent<Renderer> ();
		previousColor = rend.material.color;
		boss = GameObject.FindGameObjectWithTag ("Boss").GetComponent<Boss>();
	}
	void Update(){
		if (boss.currShootType == Boss.ShootType.Mega && once == false) {
			Debug.Log ("----MEGA");
			startinghealth = 1;
			health = 1;
			transform.parent.GetComponent<RotateAround> ().enabled = false;
			once = true;
		}
	}
	void OnEnable(){
		boss = GameObject.FindGameObjectWithTag ("Boss").GetComponent<Boss>();

		transform.localPosition = new Vector3 (transform.localPosition.x, 0, transform.localPosition.z);
		Debug.Log (transform.position);
	}
	void OnTriggerEnter(Collider col){
		if (col.tag == "Bullet") {
			if (col.GetComponent<Bullet> () != null && col.GetComponent<Bullet> ().playersBullet) {
				col.transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform.position);
			}
		}
	}

	void ReceiveDamage(){
		if (health == -1) {
			float r = 161f / 255f;
			float g = 28f / 255f;
			float b = 28f / 255f;
			float a = 145f / 255f;
			rend.material.color = new Color(r,g,b,a);
			StartCoroutine (ChangeColor ());
			return;
		}
		health--;
		if (health == 0) {
			health = startinghealth;
			rend.material.color = previousColor;
			for (int i = 0; i < startinghealth-1; i++) {
				transform.localScale += new Vector3 (0, 0, 40);
			}

			transform.parent.gameObject.SetActive (false);
		} else {
			float r = 161f / 255f;
			float g = 28f / 255f;
			float b = 28f / 255f;
			float a = 145f / 255f;
			rend.material.color = new Color(r,g,b,a);
			StartCoroutine (ChangeColor ());
		}
	}
	IEnumerator ChangeColor(){
		yield return new WaitForSeconds (0.1f);
		rend.material.color = previousColor;
		transform.localScale -= new Vector3 (0,0,40);
		transform.position -= new Vector3 (0,1f,0);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDeflectSimple : MonoBehaviour {
		private Renderer rend;
		private Color previousColor;
		void Start(){
			rend = GetComponent<Renderer> ();
			previousColor = rend.material.color;
		}
		void OnTriggerEnter(Collider col){
			if (col.tag == "Bullet") {
				col.transform.LookAt (GameObject.FindGameObjectWithTag ("Player").transform.position);
			if(col.GetComponent<Bullet>()!=null || col.GetComponent<SineBulletScript>().playersBullet)
				ReceiveDamage ();
			}
		}

		void ReceiveDamage(){
				float r = 161f / 255f;
				float g = 28f / 255f;
				float b = 28f / 255f;
				float a = 145f / 255f;
				rend.material.color = new Color(r,g,b,a);
				StartCoroutine (ChangeColor ());
		}
		IEnumerator ChangeColor(){
			yield return new WaitForSeconds (0.1f);
			rend.material.color = previousColor;
		}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDeSpawn : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void OnTriggerEnter(Collider col){
		if (col.tag == "Box") {
			Destroy (col.gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
	}
}

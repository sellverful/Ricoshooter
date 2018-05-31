using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner1 : MonoBehaviour {

	public Transform TripleBattery;
	public Transform BlueBattery;
	public Transform LongBox;
	public float angle = 55f;
	float count = 0;
	// Use this for initialization
	void Start () {
		//Instantiate (LongBox, new Vector3 (33.46f, 1.66f, 42.29f), Quaternion.identity);
		InvokeRepeating("Spawnbox", 0f, 1f);
	}
	void Spawnbox(){
		if (count == 0) {
			Instantiate (LongBox, new Vector3 (36.8725f, 2f, 93.53963f), Quaternion.AngleAxis (angle, Vector3.up));
			count = count + 1;
		}
	 else if (count == 1) {
			Instantiate (TripleBattery, new Vector3 (36.8725f, 1f, 95f), Quaternion.AngleAxis (90f, Vector3.left));
		count = count + 1;
	} else if (count == 2) {
			Instantiate (BlueBattery, new Vector3 (36.8725f, 0.5f, 95.887f), Quaternion.AngleAxis(180, Vector3.left));
			Instantiate (BlueBattery, new Vector3 (36.8725f, 0.5f, 91.887f), Quaternion.AngleAxis(180, Vector3.left));
		count = 0;
	}
	}
	// Update is called once per frame
	void Update () {

	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner2 : MonoBehaviour {

	public Transform LongBox;
	public Transform TripleBattery;
	public Transform BlueBattery;
	public float angle = 60f;
	float count = 0;
	// Use this for initialization
	void Start () {
		//Instantiate (LongBox, new Vector3 (33.46f, 1.66f, 42.29f), Quaternion.identity);
		InvokeRepeating("Spawnbox", 0f, 1f);
	}
	void Spawnbox(){
		if (count == 0) {
			Instantiate (LongBox, new Vector3 (-231.4578f, 0.9159977f, 93.69f), Quaternion.AngleAxis (angle, Vector3.up));
			count = count + 1;
		} else if (count == 1) {
			Instantiate (TripleBattery, new Vector3 (-231.4444f, 2f, 95f), Quaternion.AngleAxis (90f, Vector3.left));
			count = count + 1;
		} else if (count == 2) {
			Instantiate (BlueBattery, new Vector3 (-231.4444f, 0.2f, 95.37f), Quaternion.AngleAxis(180, Vector3.left));
			Instantiate (BlueBattery, new Vector3 (-231.4444f, 0.2f, 91.09747f), Quaternion.AngleAxis(180, Vector3.left));
			count = 0;
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
}

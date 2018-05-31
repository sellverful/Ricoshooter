using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {
	public float speed = 350.0f;
	private Vector3 startPosition;
	private Quaternion startRotation;
	// Use this for initialization
	void Start () {
		StartCoroutine (RememberMyPosition());
	}
	IEnumerator RememberMyPosition(){
		yield return new WaitForSeconds (0.05f);
		startPosition = transform.localPosition;
		startRotation = transform.localRotation;
	}
	void OnDisable(){
		transform.localPosition = startPosition;
		transform.localRotation = startRotation;
	}
	// Update is called once per frame
	void Update () {
		transform.RotateAround (transform.parent.position, Vector3.up, speed * Time.deltaTime);
	}
}

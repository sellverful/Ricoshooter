using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour {
    public Transform[] planes;
	// Use this for initialization
	void Start () {
        planes = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
	}
  
	
	// Update is called once per frame
	void Update () {
		
	}
}

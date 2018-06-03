using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackground : MonoBehaviour {
    public Transform[] planes;
    private int first;
    private int last;
    private Vector3 firstPosition;
    private Vector3 lastPosition;

    public float backgroundSize;
	// Use this for initialization
	void Start () {
        planes = new Transform[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            planes[i] = transform.GetChild(i);
            first = 0;
            last = planes.Length - 1;
            firstPosition = transform.GetChild(first).transform.position;
            lastPosition = transform.GetChild(last).transform.position;
        }
	}
  
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < transform.childCount; i++)
        {

        }
    }
}

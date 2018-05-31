using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainSpawn : MonoBehaviour {


    public Transform Train;
	// Use this for initialization
	void Start () {
        Train.gameObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == ("Player"))
        {
            Train.gameObject.SetActive(true);
        }
    }
}

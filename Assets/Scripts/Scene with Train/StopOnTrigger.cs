using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopOnTrigger : MonoBehaviour {
    public TrainMove sceneMovement;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Train")
        {
            sceneMovement.enabled = false;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLast : MonoBehaviour {
    public PlatformSpawner spawner;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            spawner.last = true;
        }
    }
}

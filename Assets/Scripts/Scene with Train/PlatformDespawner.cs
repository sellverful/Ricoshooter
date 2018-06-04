using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDespawner : MonoBehaviour {
    public GameObject destroyer;
    // Use this for initialization
    void Start()
    {
        destroyer = GameObject.Find("PlatformDestroyer");
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z< destroyer.transform.position.z)
        {
            Destroy(gameObject);
        }
    }
}

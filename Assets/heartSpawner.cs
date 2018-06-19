using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartSpawner : MonoBehaviour {
    public GameObject heart;

    public void spawn()
    {
        Instantiate(heart,transform.position,transform.rotation);
    }
}

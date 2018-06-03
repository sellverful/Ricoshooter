using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {
    public GameObject[] thePlatforms;
    public Transform generationPoint;
    public float distanceBetween;
    public bool sizeX;
    private float platformWidth;
    // Use this for initialization
    void Start()
    {
        if(!sizeX)
            platformWidth = thePlatforms[0].GetComponent<BoxCollider>().size.z * thePlatforms[0].transform.localScale.z;
        else 
            platformWidth = thePlatforms[0].GetComponent<BoxCollider>().size.x* thePlatforms[0].transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < generationPoint.position.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + platformWidth + distanceBetween);
            Instantiate(thePlatforms[Random.Range(0, thePlatforms.Length - 1)], transform.position, transform.rotation);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMiniGame : MonoBehaviour {
    public GameObject minigame;
    private PlayerController player;
    private NewSmoothCamera playerCamera;
	// Use this for initialization
	void Start () {
        minigame.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NewSmoothCamera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyUp(KeyCode.E))
        {
            minigame.SetActive(true);
            player.enabled = false;
            playerCamera.enabled = false;
        }
    }
}

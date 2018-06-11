using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMiniGame : MonoBehaviour {
    public GameObject minigame;
    public PlayerController player;
	// Use this for initialization
	void Start () {
        minigame.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        }
    }
}

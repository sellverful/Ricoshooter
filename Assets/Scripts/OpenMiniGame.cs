using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenMiniGame : MonoBehaviour {
    public GameObject minigame;
    private PlayerController player;
    private NewSmoothCamera playerCamera;
    public ActivateEnemies ifLose;

    public bool didhewin;

    public DAKKA_DAKKA_DAKKA ddd;

    Collider col;
    //public bool wasplayed;
    // Use this for initialization
    void Start () {
        minigame.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<NewSmoothCamera>();
        //ifLose.enabled = false;
        //ifLose.gameObject.SetActive(false);
        col = ifLose.gameObject.GetComponent<Collider>();
        col.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		if (minigame.activeSelf == false && didhewin == true)
        {
            ddd.enabled = false;
        }
        else if (minigame.activeSelf == false && didhewin == false)
        {
            //ifLose.gameObject.SetActive(true);
            //ifLose.enabled = true;
            col.enabled = true;
            if(ifLose.allded == true)
            {
                ddd.enabled = false;
            }
        }
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

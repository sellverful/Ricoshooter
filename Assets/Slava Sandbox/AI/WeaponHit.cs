using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour {

    private PlayerController player;

    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && player.undead == false)
        {
            player.SendMessage("DealDamage" ,1f);
        } else if (other.tag == "Player")
        {
            Debug.Log("HIT");
        }
    }
}

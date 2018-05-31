using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heartHeal : MonoBehaviour
{
    public int healAmount = 1;
    // Use this for initialization
    void Start()
    {

    }
    bool once = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !once)
        {
            other.GetComponent<PlayerController>().SendMessage("Heal", healAmount);
            GetComponent<AudioSource>().enabled = true;
            transform.localScale = Vector3.zero;
            once = true;
            Destroy(gameObject, 1);
        }
        else if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
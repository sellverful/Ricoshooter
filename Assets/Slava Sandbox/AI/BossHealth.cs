using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour {
    public float bossHealth;
    private Transform healthBar;
    // Use this for initialization
    void Start () {
        bossHealth = GetComponentInParent<RunningRiot.Boss>().currentHealth;
        healthBar = transform.Find("health");
    }
	
	// Update is called once per frame
	void Update () {
        setHealthBar(GetComponentInParent<RunningRiot.Boss>().currentHealth);
	}
    void setHealthBar(float health)
    {
        if (health > 0)
        {
            healthBar.localScale = new Vector3(health / 1000, healthBar.localScale.y, healthBar.localScale.z);
        } else
        {
            healthBar.localScale = new Vector3(0, healthBar.localScale.y, healthBar.localScale.z);
        }
        
    }
}

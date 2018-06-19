using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeWeapon : MonoBehaviour
{
	public Image image;
    public int shootType = 1;
    // Use this for initialization
    void Start()
    {

    }
    bool once = false;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (once) return;
        if (other.tag == "Player")
        {
            if(image!=null)
			    image.gameObject.SetActive (true);
            once = true;
            switch (shootType)
            {
                case 1:
                    other.GetComponentInChildren<Gun>().SendMessage("ChangeShootType", Gun.ShootType.SineShotgun);
                    break;
                case 2:
                    other.GetComponentInChildren<Gun>().SendMessage("ChangeShootType", Gun.ShootType.Around);
                    break;
                case 3:
                    other.GetComponentInChildren<Gun>().SendMessage("ChangeShootType", Gun.ShootType.DoubleGun);
                    break;
            }
            GetComponent<AudioSource>().enabled = true;
            transform.localScale = Vector3.zero;
            Destroy(gameObject, 1);
        }
       
    }
}

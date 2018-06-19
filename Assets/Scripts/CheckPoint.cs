using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject.Find("CheckpointManage").SendMessage("ChangeCheckPoint",transform);
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSmoothCamera : MonoBehaviour {
	public float Damping = 12.0f;
	public Transform Player;
	public float Height  = 13.0f;
	public float Offset  = 0.0f;
	public float ViewDistance = 10.0f;
	private Vector3 Center;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void FixedUpdate () 
	{
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = ViewDistance;
		Vector3 CursorPosition = Camera.main.ScreenToWorldPoint(mousePos);

		Vector3 PlayerPosition = Player.GetComponent<Rigidbody>().position;

		Center = new Vector3((PlayerPosition.x + CursorPosition.x) / 2, PlayerPosition.y, (PlayerPosition.z + CursorPosition.z) / 2);

		transform.position = Vector3.Lerp(transform.position, Center + new Vector3 (0, Height, Offset), Time.deltaTime * Damping); 
	}
}

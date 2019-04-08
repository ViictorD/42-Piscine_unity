using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

	public float camSens = 0.25f;
	public GameObject	player;
	private Camera		mycam;
	private Vector3 lastMouse = new Vector3(255, 255, 255);

	// Use this for initialization
	void Start () {
		this.mycam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		lastMouse = Input.mousePosition - lastMouse;
		lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
		lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
		transform.eulerAngles = lastMouse;
		lastMouse = Input.mousePosition;
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2, player.transform.position.z);
	}

}

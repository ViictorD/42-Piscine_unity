using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		//this.player = GameObject.FindGameObjectWithTag("Player");
		this.offset = player.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = this.player.transform.position - this.offset;
		transform.LookAt(this.player.transform);
	}
}

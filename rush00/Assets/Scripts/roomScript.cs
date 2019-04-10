using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomScript : MonoBehaviour {

	public int		roomNumber;
	public doorScript[]	doors;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<Player>().roomNumber = this.roomNumber;
		}
		else if (other.gameObject.tag == "bodyEnemy")
		{
			other.gameObject.transform.parent.gameObject.GetComponent<enemyScript>().roomNumber = this.roomNumber;
		}
	}

}

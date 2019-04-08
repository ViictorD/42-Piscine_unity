using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

	public GameObject	ball;
	public GameObject	flag1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("space"))
		{
			Vector3 tmp = flag1.transform.position - ball.transform.position;
			tmp = tmp.normalized;
			tmp += new Vector3(0, 1, 0);
			ball.GetComponent<Rigidbody>().AddForce(tmp * 30, ForceMode.Impulse);
		}
	}
}

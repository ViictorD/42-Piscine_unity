using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

	public GameObject	ball;
	public GameObject	flag1;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		this.placeAtBall(flag1.transform);
	}

	private void placeAtBall(Transform target)
	{
		transform.LookAt(target);
		transform.position = new Vector3(ball.transform.position.x - 3, ball.transform.position.y + 1, ball.transform.position.z);
	}
}

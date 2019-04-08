using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour {

	public float mainSpeed = 0.8f;
	private CharacterController	cc;

	// Use this for initialization
	void Start () {
		this.cc = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

		// Vector3	v = new Vector3(0, 0, Input.GetAxis("Vertical"));
		// v = transform.TransformDirection(v);
		// v *= this.mainSpeed;
		// transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * Time.deltaTime * 10);
		// this.cc.Move(v);
		Vector3	v = new Vector3();
		if (Input.GetKey(KeyCode.W))
			v += Camera.main.transform.forward.normalized;
		if (Input.GetKey(KeyCode.S))
			v += Camera.main.transform.forward.normalized * -1;
		if (Input.GetKey(KeyCode.A))
			v += Camera.main.transform.right.normalized * -1;
		if (Input.GetKey(KeyCode.D))
			v += Camera.main.transform.right.normalized;

		v = v.normalized * mainSpeed;
		Debug.Log(v);
		this.cc.Move(v.normalized);
		// .transform.Translate(v * Time.deltaTime);
	}
}

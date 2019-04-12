using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseMouvement : MonoBehaviour {

	//public GameObject crosshair;
	//private Vector3 offset;
    private float       _screenTier = Screen.width / 3;

	// Use this for initialization
	void Start () {
		//this.offset = transform.position - transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        float pos = Input.mousePosition.x;
        if (pos > 0 && pos < this._screenTier) // tourne a gauche
            transform.Rotate(new Vector3(0, -40, 0) * Time.deltaTime);
        else if (pos > this._screenTier * 2 && pos < Screen.width) // tourne a droite
            transform.Rotate(new Vector3(0, 40, 0) * Time.deltaTime);
		
		//float desiredAngle = transform.eulerAngles.y;
		//Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
		//transform.position = transform.position - (rotation * offset);
		//transform.LookAt(transform);
	}
}

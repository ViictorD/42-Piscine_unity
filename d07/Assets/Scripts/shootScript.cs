using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            // tire missile
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("test");
            // tire mitraillette
        }
	}
}

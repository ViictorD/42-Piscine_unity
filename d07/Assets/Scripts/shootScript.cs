using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootScript : MonoBehaviour {

    public AudioSource gunShot;
    public AudioSource missShot;
    public AudioSource hitShot;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
            // Audio
            this.gunShot.Play();
            // Particle on hit
		}
		else if (Input.GetMouseButtonDown(1))
		{
            // Check Ammo
			// Audio
            this.gunShot.Play();
            // particle on hit
        }
	}
}

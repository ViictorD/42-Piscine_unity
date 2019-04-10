using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootScript : MonoBehaviour {

    public AudioSource gunShot;
    public AudioSource hitShot;
    public AudioSource missShot;
    public ParticleSystem gunShotParticle;
    public ParticleSystem missileShotParticle;
    public ParticleSystem missShotParticle;
    public int missileCount = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
            // Audio
            this.gunShot.Play();
            this.gunShotParticle.Play();

            RaycastHit ray;
            if (Physics.Raycast(transform.position, transform.forward, out ray, 100))
            {
                this.missShotParticle.transform.position = ray.point;
                this.missShotParticle.Play();
                this.missShot.Play();
            }
		}
		else if (Input.GetMouseButtonDown(1))
		{
            if (this.missileCount <= 0)
                return;

            --this.missileCount;

            this.gunShot.Play();
            this.missileShotParticle.Play();
            RaycastHit ray;
            if (Physics.Raycast(transform.position, transform.forward, out ray, 100))
            {
                this.missShotParticle.transform.position = ray.point;
                this.missShotParticle.Play();
                this.missShot.Play();
            }
        }
	}
}

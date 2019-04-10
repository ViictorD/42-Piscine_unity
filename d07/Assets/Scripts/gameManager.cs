using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

    public static gameManager instance;
    public AudioSource bgMusic;

	// Use this for initialization
	void Start () {
        instance = this;
        this.bgMusic.PlayScheduled(this.bgMusic.clip.length);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

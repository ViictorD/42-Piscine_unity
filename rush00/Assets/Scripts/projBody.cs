using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projBody : MonoBehaviour {

    public float timeout = 2f;
    private float current_time = 0f;
    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity.x > 0 || rb.velocity.y > 0)
            current_time += Time.deltaTime;
        if (current_time >= timeout)
        {
            rb.velocity = new Vector2(0f, 0f);
            current_time = 0;
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour {

	public GameObject	player;
    private Vector3     offset;

	// Use this for initialization
	void Start () {
        this.offset = player.transform.position - transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        // Rotate
        float desiredAngle = this.player.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = this.player.transform.position - (rotation * offset);
        transform.LookAt(this.player.transform);

    }

	public void move()
	{
        Vector3 desiredPosition = this.player.transform.position + offset;
        Vector3 position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 1);
        transform.position = position;
        transform.LookAt(player.transform.position);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    // Use this for initialization
    public string type;
    public Rigidbody2D rb;
    public projBody projBody;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        projBody = GetComponent<projBody>();
	}

	// Update is called once per frame
	private void OnCollisionEnter2D(Collision2D collision)
	{
        characterType target = collision.gameObject.GetComponent<characterType>();
        if (target)
        {
            if (target.type == "player")
            {
                //Time.timeScale = 0f;
                gameManager.gm.gameIsOver = true;
            }
            else if (target.type != type)
                Destroy(collision.gameObject);
        }
        Destroy(gameObject);
	}
	void Update () {

        if (!projBody)
            return;
        if (rb.velocity.x <= 0f ||  rb.velocity.y <= 0f)
            Destroy(gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    public float speed;             //Floating point variable to store the player's movement speed.
    public float rotationSpeed;             //Floating point variable to store the player's movement speed.
	public int	roomNumber;
    public Animator legAnimator;
    public Vector3 target;
    public Weapon weapon;
    public GameObject weaponobj; 
    public float throwWeaponSpeed = 100;
    public float rotateSpeed = 200;
    public AudioSource dieSound;

	private DateTime	vecolityTime;
	private bool		stopVelocity = false;

    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.

    // Use this for initialization
    void Start()
    {
        //Get and store a reference to the Rigidbody2D component so that we can access it.

        rb2d = GetComponent<Rigidbody2D>();
        weaponobj = transform.GetChild(0).gameObject;
    }

    private void changeOrientation()
    {
        var mouse = Input.mousePosition;
             var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
             var offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
             var angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
             transform.rotation = Quaternion.Euler(0, 0, angle + 90);
    }

	private void OnDestroy()
	{
        dieSound.Play();
	}

	private void Update()
	{
		if (this.stopVelocity && (DateTime.Now - this.vecolityTime).TotalMilliseconds > 10)
		{
			if (this.rb2d.velocity != Vector2.zero)
			{
				Vector2 newVelo = this.rb2d.velocity;
				if (newVelo.x > 0)
					newVelo.x = newVelo.x - 2f < 0 ? 0 : newVelo.x - 2;
				else if (newVelo.x < 0)
					newVelo.x = newVelo.x + 2f > 0 ? 0 : newVelo.x + 2;
				if (newVelo.y > 0)
					newVelo.y = newVelo.y - 2 < 0 ? 0 : newVelo.y - 2;
				else if (newVelo.y < 0)
					newVelo.y = newVelo.y + 2 > 0 ? 0 : newVelo.y + 2;
				this.rb2d.velocity = newVelo;
			}
			else
			{
				this.stopVelocity = false;
			}
		}
        changeOrientation();

        if (!weapon)
            return;
        Vector3 shootDirection = Input.mousePosition;
            shootDirection.z = 0.0f;
            shootDirection = Camera.main.ScreenToWorldPoint(shootDirection);            
        if (Input.GetKey(KeyCode.Mouse0))
        {
            weapon.Shot(shootDirection, Input.mousePosition);

        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            weapon.Drop();
            shootDirection = shootDirection - transform.position;
            weapon.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * throwWeaponSpeed, shootDirection.y * throwWeaponSpeed);
            weapon = null;
            return;
        }
        weapon.transform.position = weaponobj.transform.position;
        weapon.transform.rotation = weaponobj.transform.rotation;
        weapon.projectile.gameObject.layer = 12;
	}


	//FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void move()
    {
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
		{
			this.rb2d.velocity = new Vector2(0, 0);
			this.vecolityTime = DateTime.Now;
			this.stopVelocity = true;

		}
        if (Input.GetKey(KeyCode.A))
			this.rb2d.velocity += new Vector2(-10, 0);
        if (Input.GetKey(KeyCode.D))
			this.rb2d.velocity += new Vector2(10, 0);
        if (Input.GetKey(KeyCode.W))
			this.rb2d.velocity += new Vector2(0, 10);
        if (Input.GetKey(KeyCode.S))
			this.rb2d.velocity += new Vector2(0, -10);
        if (rb2d.velocity.x >0 || rb2d.velocity.y>0)
        {
            legAnimator.SetTrigger("walk");
        }
        else{
            legAnimator.SetTrigger("wait");
        }

    }

	void FixedUpdate()
    {
        move();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    // Use this for initialization
    public Projectile projectile;
    public float maxSpeed;
    public Projectile clone;
    public int projectileCount;
    public int projectileCountMax;
    public bool unlimitedProjectile;
    public AudioSource shotSound;
    public SpriteRenderer currentSprite;
    public Sprite equipedSprite;
    public Sprite droppedSprite;
    public AudioSource pickupArmSound;
    public LayerMask layer;
    public string weaponType;

    public float shotTimeout = 4f;
    public float shotTimeoutMax = 4f;

    public bool killOnThrow;
    public Rigidbody2D rb;

	void Start () {
        rb = gameObject.GetComponent<Rigidbody2D>();
        currentSprite = gameObject.GetComponent<SpriteRenderer>();
        currentSprite.sprite = droppedSprite;
	}

	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D collision)
	{
        enemyScript enemy = collision.gameObject.GetComponent<enemyScript>();

        if (!enemy)
            return;
        if (killOnThrow)
        {
            enemyScript self = gameObject.GetComponentInParent<enemyScript>();
            if (!self || enemy.gameObject == self.gameObject)
                return;
            Destroy(enemy.gameObject);
        }
        // else
        // {
        //     enemy.stunned = true;
        //     Debug.Log("Ennemy  is stuned");
        // }
	}

	private void OnTriggerStay2D(Collider2D other)
	{
        Player player = other.gameObject.GetComponent<Player>();
        if (!player)
            return;
        if (Input.GetKeyDown(KeyCode.E) && !player.weapon)
        {
            other.gameObject.GetComponent<Player>().weapon = this; 
            other.gameObject.GetComponentInChildren<SpriteRenderer>().sprite = currentSprite.sprite;
            layer = 12;
            projectile.type = "player";
            pickupArmSound.Play();
            Equip();
        }
	}

    public void Drop()
    {
        if (droppedSprite)
            currentSprite.sprite = droppedSprite; 
    }

    public void Equip()
    {
        if (equipedSprite)
            currentSprite.sprite = equipedSprite;
    }

    public void Shot(Vector3 shootDirection, Vector3 lookDirection)
    {
    
        if (!unlimitedProjectile && projectileCount <= 0)
            return;

        if (shotTimeout < 0)
        {
            shotSound.Play();
            shootDirection = shootDirection - transform.position;
            clone = Instantiate(projectile, transform.position, transform.rotation);
            clone.GetComponent<Rigidbody2D>().velocity = new Vector2(shootDirection.x * maxSpeed, shootDirection.y * maxSpeed);


            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 offset = new Vector2(lookDirection.x - screenPoint.x, lookDirection.y - screenPoint.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            //clone.GetComponent<LayerMask>() = layer;
            clone.gameObject.layer = layer;
            clone.transform.rotation = Quaternion.Euler(0, 0, angle );

            //clone.transform.rotation = rot;
            shotTimeout = shotTimeoutMax;
            projectileCount--;
        }
    }

	void Update ()
    {
        shotTimeout -= Time.deltaTime;
    }
}

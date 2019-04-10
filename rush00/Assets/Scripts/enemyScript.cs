using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class enemyScript : MonoBehaviour {

	public Animator		legAnimator;
    public Weapon 		weapon;
    public GameObject	weaponObj;
	public int			roomNumber;
	public int			speed = 5;
	public int			timeStopChasing = 3;
	public GameObject	path;
	public bool			lockMove = false;

	private Coroutine	_pathRoutine;
	private bool		_isFollowingPath = false;
	private Coroutine	_inMovingRoutine;
	private bool		_inMoving = false;
	private DateTime	_startChase;


    public SpriteRenderer head;
    public bool stunned;

	// Use this for initialization
	void Start () {
		weapon = Instantiate(gameManager.gm.weapons[UnityEngine.Random.Range(0, gameManager.gm.weapons.Count - 1)], transform.position, Quaternion.identity);
		weaponObj.GetComponent<SpriteRenderer>().sprite = weapon.droppedSprite;
		//weapon.Equip();
		weapon.currentSprite.sprite = null;
		head = transform.Find("HeadEnemy1").GetComponent<SpriteRenderer>();
		if (head)
			head.sprite = gameManager.gm.enemyHead[UnityEngine.Random.Range(0, gameManager.gm.enemyHead.Count - 1)];
		weapon.unlimitedProjectile = true;
		weapon.layer = 13;
		weapon.projectile.type = "ennemy";

		if (this.path != null)
		{
			this._isFollowingPath = true;
			List<Vector3>	checkpoint = new List<Vector3>();
			int nb_child = this.path.transform.childCount;

			for (int i = 0; i < nb_child; i++)
				checkpoint.Add(this.path.transform.GetChild(i).transform.position);
			this._pathRoutine = StartCoroutine(this.movePath(checkpoint));
		}
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		if (this._inMoving && this._isFollowingPath == false)
		{
			weapon.Equip();
			weapon.shotTimeout -= Time.deltaTime;
			weapon.transform.position = weaponObj.transform.position;
			weapon.transform.rotation = weaponObj.transform.rotation;
			weapon.Shot(gameManager.gm.player.transform.position, gameManager.gm.player.transform.position);
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.name == "Player")
		{
			if (!Physics2D.Linecast(transform.position, other.gameObject.transform.position, LayerMask.GetMask("map")) &&
				!Physics2D.Linecast(transform.position, other.gameObject.transform.position, LayerMask.GetMask("door")))
			{
				this.checkIfOnPath();
				if (!this._inMoving)
					this.runToPlayer();
			}
		}
	}

	public void	checkIfOnPath()
	{
		if (this._isFollowingPath)
		{
			StopCoroutine(this._pathRoutine);
			if (this._inMoving)
				StopCoroutine(this._inMovingRoutine);
			this._inMoving = false;
			this.lockMove = false;
			this._isFollowingPath = false;
		}
	}

	IEnumerator	movePath(List<Vector3> checkpoints)
	{
		while (true)
		{
			foreach (Vector3 v in checkpoints)
			{
				this.move(v);
				while (this._inMoving)
					yield return new WaitForSeconds(0.5f);
			}
		}
	}

	public void move(Vector3 pos)
	{
		this.rotateSprite(pos);
		if (!this._inMoving)
			this._inMovingRoutine = StartCoroutine(this.moveRoutine(pos, false));
	}

	private void rotateSprite(Vector3 pos)
	{
		Vector3 tmp = pos - transform.position;
		float angle = Mathf.Atan2(tmp.y, tmp.x) * Mathf.Rad2Deg + 90f;
		Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
		transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 1000);
	}

	IEnumerator moveRoutine(Vector3 destPos, bool followPlayer)
	{
		bool	playerMovedRoom = false;
		this._inMoving = true;
		this.legAnimator.SetTrigger("walk");
		weapon.shotTimeout += Time.deltaTime;
		while (transform.position != destPos)
		{
			if (!this._isFollowingPath && (DateTime.Now - this._startChase).TotalSeconds > this.timeStopChasing)
			{
				if (!followPlayer)
				{
					StopCoroutine(this._pathRoutine);
					this.lockMove = false;
				}
				break ;
			}
			transform.position = Vector3.MoveTowards(transform.position, destPos, this.speed * Time.deltaTime);
			yield return new WaitForSeconds(0.01f);
			if (followPlayer)
			{
				if (this.roomNumber != gameManager.gm.player.roomNumber)
				{
					playerMovedRoom = true;
					break ;
				}
				destPos = gameManager.gm.player.transform.position;
				this.rotateSprite(destPos);
			}
		}
		this.legAnimator.SetTrigger("wait");
		if (followPlayer)
			this.lockMove = false;
		this._inMoving = false;
		if (playerMovedRoom)
			this.runToPlayer();
	}

	public void	runToPlayer()
	{
		if (this.lockMove)
			return ;

		this._startChase = DateTime.Now;

		this.lockMove = true;
		
		if ((this.roomNumber == gameManager.gm.player.roomNumber) || (this.roomNumber < 1 || this.roomNumber > 7))
		{
			this._inMovingRoutine = StartCoroutine(this.moveRoutine(gameManager.gm.player.transform.position, true));
			return ;
		}


		List<List<doorScript>> solutions = new List<List<doorScript>>();
		List<doorScript> path = new List<doorScript>();

		this.pathfinder(solutions, path, this.getRoomScript(this.roomNumber), 0);

		int best = 999;
		foreach (List<doorScript> item in solutions)
		{
			if (item.Count < best)
			{
				best = item.Count;
				path = item;
			}
		}
		this._pathRoutine = StartCoroutine(this.moveDoors(path));
	}

	IEnumerator moveDoors(List<doorScript> path)
	{
		foreach (doorScript d in path)
		{
			this.move(d.centerPoint.transform.position);
			while (this._inMoving)
				yield return new WaitForSeconds(0.5f);
		}
		this.lockMove = false;
		this.runToPlayer();
	}

	void	pathfinder(List<List<doorScript>> solutions, List<doorScript> currPath, roomScript room, int lastRoomNumber)
	{
		int nb;
		foreach (doorScript d in room.doors)
		{
			nb = d.connectRoom[0] != room.roomNumber ? d.connectRoom[0] : d.connectRoom[1];
			if (nb == gameManager.gm.player.roomNumber)
			{
				currPath.Add(d);
				solutions.Add(new List<doorScript>(currPath));
				currPath.RemoveAt(currPath.Count - 1);
				break ;
			}
			else if (nb != lastRoomNumber)
			{
				currPath.Add(d);
				this.pathfinder(solutions, currPath, this.getRoomScript(nb), room.roomNumber);
				currPath.RemoveAt(currPath.Count - 1);
			}
		}
	}

	roomScript	getRoomScript(int roomNumber)
	{
		foreach (roomScript r in gameManager.gm.mapRooms)
		{
			if (r.roomNumber == roomNumber)
				return r;
		}
		return null;
	}
}

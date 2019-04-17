using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyScript : Character {

	public bool isDead = false;
	private Animator animator;
	NavMeshAgent nm;
	Coroutine attack;
	bool isAttacking = false;

	bool runToPlayer = false;
	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator>();
		this.nm = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (isDead)
			return;
		float distance = Vector3.Distance(transform.position, gameManager.instance.player.transform.position);
		if (this.runToPlayer)
		{
			if (distance < 2)
			{
				if (!this.isAttacking)
				{
					this.nm.destination = transform.position;
					this.isAttacking = true;
					this.animator.SetTrigger("EnemyIdle");
					this.attack = StartCoroutine(this.attackRoutine());
				}
			}
			else
			{
				if (this.isAttacking)
				{
					this.isAttacking = false;
					StopCoroutine(this.attack);
				}
				this.animator.SetTrigger("EnemyRun");
				this.nm.destination = gameManager.instance.player.transform.position;
			}
		}
		else if (distance < 8)
			this.runToPlayer = true;
	}

	IEnumerator attackRoutine()
	{
		while (true)
		{
			transform.LookAt(gameManager.instance.player.transform);
			this.animator.SetTrigger("EnemyAttack");
			yield return new WaitForSeconds(0.5f);
			gameManager.instance.player.GetComponent<mayaScript>().getHit();
			yield return new WaitForSeconds(0.5f);
		}
	}

	public void getHit()
	{
		Debug.Log("NOW");
		if (--this.life <= 0)
		{
			this.isDead = true;
			if (this.isAttacking)
			{
				StopCoroutine(this.attack);
				this.isAttacking = false;
			}
			this.animator.SetTrigger("EnemyDeath");
			Invoke("death", 3.3f);
		}
	}

	void death()
	{
		Destroy(gameObject);
	}
}

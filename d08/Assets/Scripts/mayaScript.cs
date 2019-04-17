using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mayaScript : Character {

	enum characterState { IDLE, RUN, ATTACK, DIE };

	private Animator animator;
	private NavMeshAgent navMesh;
	private characterState state;
	GameObject target;
	bool isLeftClicking = false;

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator>();
		this.navMesh = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0) && !this.isLeftClicking)
		{
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit ray;
			if (Physics.Raycast(r, out ray))
			{
				if (ray.transform.gameObject.tag == "Enemy")
				{
					this.isLeftClicking = true;
					this.target = ray.transform.gameObject;
					if (Vector3.Distance(this.target.transform.position, gameManager.instance.player.transform.position) < 2)
						return;
				}
				else
					this.target = null;
				this.navMesh.destination = ray.point;
				if (this.state != characterState.RUN)
				{
					this.animator.SetTrigger("mayaRun");
					this.state = characterState.RUN;
				}
			}
		}

		if (!Input.GetMouseButton(0) && this.isLeftClicking)
			this.isLeftClicking = false;

		if (this.state == characterState.RUN && this.navMesh.destination == transform.position)
		{
			this.animator.SetTrigger("mayaIdle");
			this.state = characterState.IDLE;
		}

		if ((this.state == characterState.RUN || this.state == characterState.IDLE) && this.target != null)
		{
			if (Vector3.Distance(this.target.transform.position, gameManager.instance.player.transform.position) < 2
			    && this.state != characterState.ATTACK)
			{
				this.navMesh.destination = transform.position;
				StartCoroutine(this.attackRoutine());
				// Oublie pas de remetre dans le prefab enemy
			}
			else
				this.navMesh.destination = this.target.transform.position;
		}
	}

	IEnumerator attackRoutine()
	{
		do
		{
			transform.LookAt(this.target.transform);
			this.state = characterState.ATTACK;
			this.animator.SetTrigger("mayaAttack");
			yield return new WaitForSeconds(0.1f);
			this.target.GetComponent<enemyScript>().getHit();
			yield return new WaitForSeconds(0.5f);
		} while (this.target != null && !this.target.GetComponent<enemyScript>().isDead && Input.GetMouseButton(0));
		this.state = characterState.IDLE;
		this.animator.SetTrigger("mayaIdle");
		this.target = null;
	}

	public void getHit()
	{
		if (--this.life <= 0)
		{
			// dead
		}
	}
}

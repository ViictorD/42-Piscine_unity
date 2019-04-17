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

	// Use this for initialization
	void Start () {
		this.animator = GetComponent<Animator>();
		this.navMesh = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit ray;
			if (Physics.Raycast(r, out ray))
			{
				if (ray.transform.gameObject.tag == "Enemy")
					this.target = ray.transform.gameObject;
				else
					this.target = null;
				this.navMesh.destination = ray.point;
				this.animator.SetTrigger("mayaRun");
				this.state = characterState.RUN;
			}
		}

		if (this.state == characterState.RUN && this.navMesh.destination == transform.position)
		{
			this.animator.SetTrigger("mayaIdle");
			this.state = characterState.IDLE;
		}

		if (this.state == characterState.RUN && this.target != null)
		{
			if (Vector3.Distance(this.target.transform.position, gameManager.instance.player.transform.position) < 2
			    && this.state != characterState.ATTACK)
			{
				Debug.Log("Test"); // call plein de fois
				this.navMesh.destination = transform.position;
				this.state = characterState.ATTACK;
				StartCoroutine(this.attackRoutine());
				// Oublie pas de remetre dans le prefab enemy
			}
			else
				this.navMesh.destination = this.target.transform.position;
		}
	}

	IEnumerator attackRoutine()
	{
		transform.LookAt(this.target.transform);
		this.animator.SetTrigger("mayaAttack");
		yield return new WaitForSeconds(0.5f);
		this.target.GetComponent<enemyScript>().getHit();
		yield return new WaitForSeconds(1f);
		this.animator.SetTrigger("mayaIdle");
	}

	public void getHit()
	{
		if (--this.life <= 0)
		{
			// dead
		}
	}
}

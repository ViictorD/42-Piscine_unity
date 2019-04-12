using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class mayaScript : MonoBehaviour {

	enum characterState { IDLE, RUN, ATTACK, DIE };

	private Animator animator;
	private NavMeshAgent navMesh;
	private characterState state;


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
				// Mettre une rotation du perso vers la ou on a clique
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
	}
}

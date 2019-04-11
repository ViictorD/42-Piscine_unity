using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyScript : MonoBehaviour {

	private GameObject target = null;
	private bool isInRange = false;
	private NavMeshAgent navMesh;

	// Use this for initialization
	void Start () {
		this.navMesh = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
		this.getClosestEnemy();
		if (this.target == null)
			return;
		if (this.isInRange
		    && transform.position.y < target.transform.position.y + 0.5
		    && transform.position.y > target.transform.position.y + -0.5)
		{
			this.navMesh.destination = transform.position;
			// Vise et tire
			RaycastHit ray;
			if (Physics.Raycast(transform.position, transform.forward, out ray, 100))
			{
				if (ray.collider.gameObject == this.target)
				{
					Debug.Log("Shooot");
					// TIRE (son + particule)
				}
				Vector3 lookPos = this.target.transform.position - transform.position;
				lookPos.y = 0;
				Quaternion rotation = Quaternion.LookRotation(lookPos);
				transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
			}
		}
		else
			this.navMesh.destination = target.transform.position;
	}

	private void OnTriggerStay(Collider other)
	{
		if (this.target != null && this.target == other.gameObject)
			this.isInRange = true;
	}

	private void getClosestEnemy()
	{
		GameObject save = null;
		float distance = 9999999;
		foreach (GameObject g in gameManager.instance.enemy)
		{
			if (g == this)
				continue;
			float d = Vector3.Distance(g.transform.position, transform.position);
			if (d < distance)
			{
				distance = d;
				save = g;
			}
		}
		if (this.target != save)
		{
			this.target = save;
			this.isInRange = false;
		}
		else
			this.target = save;
	}
}

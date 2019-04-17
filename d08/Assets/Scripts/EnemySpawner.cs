using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	public GameObject femaleEnemy;
	public GameObject maleEnemy;
	public List<GameObject> posSpawn;
	public float spawnTime = 4;

	private GameObject[] currentEnemy;
	private bool waitSpawn = false;

	// Use this for initialization
	void Start () {
		this.currentEnemy = new GameObject[this.posSpawn.Count];
		for (int i = 0; i < this.posSpawn.Count; i++)
			this.currentEnemy[i] = Instantiate(Random.Range(0, 2) < 1 ? this.maleEnemy : this.femaleEnemy, this.posSpawn[i].transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		if (!this.waitSpawn)
		{
			this.waitSpawn = true;
			Invoke("newEnemy", this.spawnTime);
		}
	}

	void newEnemy()
	{
		this.waitSpawn = false;
		for (int i = 0; i < this.posSpawn.Count; i++)
		{
			if (this.currentEnemy[i] == null)
			{
				this.currentEnemy[i] = Instantiate(Random.Range(0, 2) < 1 ? this.maleEnemy : this.femaleEnemy, this.posSpawn[i].transform.position, Quaternion.identity);
				return;
			}
		}
	}
}

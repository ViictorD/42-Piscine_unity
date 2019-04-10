using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySound : MonoBehaviour {

	public enemyScript	enemy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (enemy.gameObject.transform.position != this.transform.position)
			transform.position = this.enemy.transform.position;
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (other.gameObject.tag == "Player")
		{
			if (gameManager.gm.player.weapon && gameManager.gm.player.weapon.shotSound.isPlaying && !this.enemy.lockMove)
			{
				this.enemy.checkIfOnPath();
				this.enemy.runToPlayer();
			}
		}
	}
}

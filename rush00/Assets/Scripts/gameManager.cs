using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour {

    // Use this for initialization
    public static gameManager gm;
    public Player player;
    public GameObject[] enemies;
    public enemyScript Boss;
    public List<Weapon> weapons;
    public List<Sprite> enemyHead;
	public List<roomScript>		mapRooms;

    public Canvas endGameMenu;
    public AudioSource winSound;
    public AudioSource ambiance;
    public AudioSource loseSound;
    public UnityEngine.UI.Text endText;
    public bool gameIsOver = false;
    public bool soundHasBeenPlayed = false;


	private void Awake()
	{
        if (!gm)
            gm = this;
	}

	void Start () {
        Time.timeScale = 1f;
        ambiance.Play();
        endGameMenu.gameObject.GetComponent<Canvas>().enabled = false;
	}

    public void Win()
    {
        endGameMenu.gameObject.GetComponent<Canvas>().enabled = true;
        endText.text = "Victory";
        winSound.Play();
        soundHasBeenPlayed = true;
    }

    public void Lose()
    {
        endGameMenu.gameObject.GetComponent<Canvas>().enabled = true;
        endText.text = "Game Over";
        loseSound.Play();
        soundHasBeenPlayed = true;
    }

	void Update ()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (soundHasBeenPlayed)
            return;
        if (gameIsOver)
        {
            Lose();
        }
        if (!Boss || enemies.Length <= 0)
        {
            Win();
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponUi : MonoBehaviour {

	// Use this for initialization
    public UnityEngine.UI.Text weaponName;
    public UnityEngine.UI.Text munitionNumber;
    public UnityEngine.UI.Text weaponType;
    public Player player;

	void Start ()
    {
        player = gameManager.gm.player;
        weaponName.text = "No Weapon";
        munitionNumber.text = "-";
        weaponType.text = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (!player || !player.weapon)
        {
            weaponName.text = "No Weapon";
            munitionNumber.text = "-";
            weaponType.text = "";
            return;
        }
        weaponName.text = player.weapon.gameObject.name;
        munitionNumber.text = (player.weapon.unlimitedProjectile ? "Unlimited" :  player.weapon.projectileCount.ToString() + "/" + player.weapon.projectileCountMax);
        weaponType.text = player.weapon.weaponType;
	}
}

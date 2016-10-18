using UnityEngine;
using System.Collections;

public class HumanController {


	GameObject gameObject;
	PlayerShoot shoot;
	PlayerLight light;
	PlayerInteract interact;
	PlayerBuffs buffs;
	PlayerTraps traps;
	PlayerAbility1 ability1;

	public HumanController(GameObject gameObject){
		this.gameObject = gameObject;
		shoot = gameObject.GetComponent<PlayerShoot> ();
		light = gameObject.GetComponent<PlayerLight> ();
		interact = gameObject.GetComponent<PlayerInteract> ();
		buffs = gameObject.GetComponent<PlayerBuffs> ();
		traps = gameObject.GetComponent<PlayerTraps> ();
		ability1 = gameObject.GetComponent<PlayerAbility1> ();
	}

	public void Update () {
		if (Input.GetKey (KeyMap.fireKey)) {
			shoot.Shoot ();
		}
		if (Input.GetKey (KeyMap.lightKey)) {
			light.Toggle ();
		}
		if (Input.GetKeyDown (KeyMap.interactKey)) {
			/* Note : here we use GetKeyDown instead of GetKey */
			interact.Interact ();
		}
		if (Input.GetKey (KeyMap.buffLifeKey)) {

		}
		if (Input.GetKey (KeyMap.buffSpeedKey)) {

		}
		if (Input.GetKey (KeyMap.buffStrenghtKey)) {

		}
		if (Input.GetKey (KeyMap.ability1)) {

		}
		if (Input.GetKey (KeyMap.ability2)) {

		}
	}
}

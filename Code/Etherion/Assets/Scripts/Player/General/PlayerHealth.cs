using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : Health
{



	Animator anim;
	Collider col;
	Renderer rend;

	public float deathAnimLength = 1.2f;


	public void Start ()
	{
		base.Start ();
		anim = transform.GetComponentInChildren<Animator> ();
		col = GetComponent<Collider> ();
		rend = GetComponentInChildren<Renderer> ();
	}

	public override void Update ()
	{
		base.Update ();

		if (dead && timer > deathAnimLength) {
			col.enabled = false;
			rend.enabled = false;
			if (player.isHuman) {
				//Do something wih the camera
			}
		}

		if (dead && timer > GameManager.RESPAWN_DELAY) {
			Respawn ();
		}
	}

	public override void Death ()
	{
		anim.SetTrigger ("Die");
	}

	public void Respawn ()
	{
		dead = false;
		life = maxLife;
		col.enabled = true;
		rend.enabled = true;
		anim.SetTrigger ("Idle");
		transform.position = player.team.teamSlot.GetRandomSpawn ().transform.position;
		if (nav!=null){
			nav.enabled = true;
		}
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : Health
{



	Animator anim;
	Collider col;
	Renderer[] rend;

	public float deathAnimLength = 1.2f;

	public bool spawnTeleport;

	public void Start ()
	{
		base.Start ();
		anim = transform.GetComponentInChildren<Animator> ();
		col = GetComponent<Collider> ();
		rend = GetComponentsInChildren<Renderer> ();
		spawnTeleport = false;
	}

	public override void Update ()
	{
		base.Update ();

		if (dead && timer > deathAnimLength) {
			col.enabled = false;

			foreach (Renderer r in rend) {
				r.enabled = false;
			}
			if (player.isHuman) {
				//Do something wih the camera
			}
			if (!spawnTeleport) {
				transform.position = player.team.teamSlot.GetRandomSpawn ().transform.position;
				spawnTeleport = true;
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

	public void LevelUp ()
	{
		
	}

	public void Respawn ()
	{
		dead = false;
		life = maxLife;
		col.enabled = true;
		spawnTeleport = false;

		foreach (Renderer r in rend) {
			r.enabled = true;
		}

		anim.SetTrigger ("Idle");

		if (nav != null) {
			nav.enabled = true;
		}
	}
}

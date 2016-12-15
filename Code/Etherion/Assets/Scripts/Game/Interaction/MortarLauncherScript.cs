using UnityEngine;
using System.Collections;

public class MortarLauncherScript : Interaction
{



	public float interval;

	public float speed;
	public float range;
	public float acceleration;
	public float ascendingTime;
	public float damage;
	public float damageRadius;
	public float damageDecrease;

	public float deathDelay;
	public float minDamage;

	public float dispertion;

	public GameObject barrel;
	public GameObject prefab;
	AudioSource audio;
	float timer;

	void Start ()
	{
		timer = interval;
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
	}

	public override void Action ()
	{
		if (timer > interval) {
			Player player = initiator.GetComponent<Player> ();
			if (player != null) {
				Mortar.Create (prefab, barrel.transform, speed, dispertion, acceleration, ascendingTime, 1000f, deathDelay, damage, minDamage, damageDecrease, true, player.isHuman, player, damageRadius);

				timer = 0f;
				audio.Play ();
			}
		}
	}
}

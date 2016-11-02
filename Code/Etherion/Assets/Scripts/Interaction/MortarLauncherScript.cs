using UnityEngine;
using System.Collections;

public class MortarLauncherScript : Interaction {



	public float interval;

	public float speed;
	public float range;
	public float acceleration;
	public float ascendingTime;
	public float damage;
	public float damageRadius;
	public float damageDecrease;

	public GameObject barrel;
	public GameObject prefab;
	AudioSource audio;
	float timer;

	void Start () {
		timer = interval;
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
	}

	public override void Action(){
		if (timer > interval) {
			PlayerState state = initiator.GetComponent<PlayerState> ();
			if (state != null) {
				Mortar.Create (gameObject, prefab, barrel.transform, speed, range, state.side, acceleration, ascendingTime, damage, damageRadius, damageDecrease, true);
				timer = 0f;
				audio.Play ();
			}
		}
	}
}

using UnityEngine;
using System.Collections;

public class FlareShot : Projectile {

	protected float gravityDeviation = .3f;
	protected Quaternion targetRotation;

	protected float acceleration;
	protected float ascendingTime;
	protected float descendingSpeed;
	protected float intensity;
	protected float lightRange;
	protected float deathDelay;
	protected Light flareLight;
	protected Flare flare;
	protected bool descentTrigger;
	protected bool dead;


	public static GameObject Create(GameObject owner, GameObject prefab, Transform barrel, float speed, float range, float acceleration, float ascendingTime, float descendingSpeed,float intensity, float lightRange,float deathDelay){
		GameObject projectile = Projectile.Create (owner, prefab, barrel,speed);
		FlareShot flareShot = projectile.GetComponent<FlareShot> ();
		flareShot.acceleration = acceleration;
		flareShot.ascendingTime = ascendingTime;
		flareShot.descendingSpeed = descendingSpeed;
		flareShot.intensity = intensity;
		flareShot.lightRange = lightRange;
		flareShot.deathDelay = deathDelay;
		flareShot.flareLight = projectile.GetComponent<Light> ();
		flareShot.flare = flareShot.flareLight.flare;
		flareShot.flareLight.flare = null;
		flareShot.descentTrigger = false;
		flareShot.dead = false;
		flareShot.targetRotation = Quaternion.Euler (90f, 0f, 0f);
		return projectile;
	}


	void FixedUpdate () {
		if (dead)
			return;
		
		base.FixedUpdate ();

		if (timer < ascendingTime) {
			speed += acceleration * Time.fixedDeltaTime;
		} else {
			if (!descentTrigger) {
				descentTrigger = true;
				flareLight.flare = flare;
				flareLight.intensity = intensity;
				flareLight.range = lightRange;
				speed = descendingSpeed;
			}

			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, gravityDeviation * Time.fixedDeltaTime);
		}


	}

	protected override void OnTriggerEnter(Collider other) {
		Delete (deathDelay);
		dead = true;
	}
}

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

	protected Light flareLight;

	protected Flare flare;

	protected bool descentTrigger;



	public static GameObject Create(GameObject owner, GameObject prefab, Transform barrel, float speed, float range, float acceleration, float ascendingTime, float descendingSpeed,float intensity, float lightRange){
		GameObject projectile = Projectile.Create (owner, prefab, barrel,speed,range);
		FlareShot flareShot = projectile.GetComponent<FlareShot> ();
		flareShot.acceleration = acceleration;
		flareShot.ascendingTime = ascendingTime;
		flareShot.descendingSpeed = descendingSpeed;
		flareShot.intensity = intensity;
		flareShot.lightRange = lightRange;
		flareShot.flareLight = projectile.GetComponent<Light> ();
		flareShot.flare = flareShot.flareLight.flare;
		flareShot.flareLight.flare = null;
		flareShot.descentTrigger = false;
		flareShot.targetRotation = Quaternion.Euler (90f, 0f, 0f);
		return projectile;
	}


	void FixedUpdate () {
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

		base.FixedUpdate ();
	}
}

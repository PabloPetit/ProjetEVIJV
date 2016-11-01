using UnityEngine;
using System.Collections;

public class Flare : Projectile {

	float gravityDeviation = 2f;

	protected float acceleration;
	protected float ascendingTime;
	protected float descendingSpeed;
	protected float intensity;
	protected float lightRange;

	protected Light flareLight;


	public static GameObject Create(GameObject owner, GameObject prefab, Transform barrel, float speed, float range, float acceleration, float ascendingTime, float descendingSpeed,float intensity, float lightRange){
		GameObject projectile = Projectile.Create (owner, prefab, barrel,speed,range);
		Flare flare = projectile.GetComponent<Flare> ();
		flare.acceleration = acceleration;
		flare.ascendingTime = ascendingTime;
		flare.descendingSpeed = descendingSpeed;
		flare.intensity = intensity;
		flare.lightRange = lightRange;
		flare.flareLight = projectile.GetComponent<Light> ();
	}


	void FixedUpdate () {
		if (timer < ascendingTime) {
			speed += acceleration * Time.fixedDeltaTime;
		} else {
			flareLight.intensity = intensity;
			flareLight.range = lightRange;
			// Allumer le flare
			transform.rotation = Quaternion.LookRotation (Vector3.RotateTowards (transform.forward, transform.position + Vector3.down * 10f, gravityDeviation * Time.fixedDeltaTime, 0.0f));
			speed = descendingSpeed;
		}

		base.FixedUpdate ();
	}
}

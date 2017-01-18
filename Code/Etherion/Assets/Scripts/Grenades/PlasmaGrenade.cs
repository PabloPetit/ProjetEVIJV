using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasmaGrenade : Grenade
{


	public static float EFFECTS_TIME = 1.9f;
	public static float LIGHT_TIME = .75f;

	public float blinkFreq = 10f;

	float timer;

	Light pointLight;

	public override void Start ()
	{
		base.Start ();
		deathDelay = EFFECTS_TIME;
		timer = 0f;
	}



	void Update ()
	{

		if (hasCollide) {

			if (pointLight == null)
				pointLight = GetComponentInChildren<Light> ();

			timer += Time.deltaTime;

			if (timer < LIGHT_TIME) {

				bool on = timer % (1f / blinkFreq) > 1f / (blinkFreq * 2f);
				pointLight.enabled = on;
			} else {
				pointLight.enabled = false;
			}
		}
	}

	public static GameObject Create (GameObject prefab, Vector3 position, Vector3 velocity, Player shooter, bool friendlyFire, float initialDamage, float minDamage, float damageDecrease)
	{
		return Grenade.Create (prefab, position, velocity, shooter, friendlyFire, initialDamage, minDamage, damageDecrease);
	}


}

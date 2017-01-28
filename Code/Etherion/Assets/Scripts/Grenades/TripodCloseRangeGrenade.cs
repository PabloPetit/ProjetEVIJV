using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripodCloseRangeGrenade : Grenade
{


	public static float EFFECTS_TIME = 1.9f;

	public override void Start ()
	{
		base.Start ();
		deathDelay = EFFECTS_TIME;
	}


	public static GameObject Create (GameObject prefab, Vector3 position, Vector3 velocity, Player shooter, bool friendlyFire, float initialDamage, float minDamage, float damageDecrease)
	{
		return Grenade.Create (prefab, position, velocity, shooter, friendlyFire, initialDamage, minDamage, damageDecrease);
	}

}

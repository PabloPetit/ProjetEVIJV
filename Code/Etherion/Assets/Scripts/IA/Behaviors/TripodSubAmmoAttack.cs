using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripodSubAmmoAttack : IABehavior
{
	public static float SUB_VALUE = 50f;

	TripodController trip;

	public TripodSubAmmoAttack (IA ia) : base (ia)
	{
		trip = (TripodController)ia;
	}

	public override void Run ()
	{
		trip.weaponSystem.ShootSubAmmoAttack ();
	}

	public override float EvaluatePriority ()
	{
		float val = 0;


		foreach (Player p in trip.enemiesAround) {
			float dist = Vector3.Distance (ia.transform.position, p.transform.position);

			if (dist > trip.weaponSystem.subAmmoMinDist) {
				val += SUB_VALUE;
				break;
			}
		}

		val *= (trip.weaponSystem.isSubAmmoAttackReady ()) ? 1f : 0f;

		return val;
	}
}

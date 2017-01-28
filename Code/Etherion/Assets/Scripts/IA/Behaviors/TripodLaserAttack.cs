using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripodLaserAttack : IABehavior
{

	public static float LASER_ATTACK_PRIORITY = 5f;

	TripodController trip;

	public TripodLaserAttack (IA ia) : base (ia)
	{
		trip = (TripodController)ia;
	}


	public override void Run ()
	{

	
		Player p = null;
		float dist = trip.visionRadius + 1f;
		foreach (Player t in trip.enemiesAround) {
			float tmp = Vector3.Distance (trip.transform.position, t.gameObject.transform.position);
			if (tmp < dist) {
				p = t;
				dist = tmp;
			}
		}
		if (p != null) {
			trip.weaponSystem.target = p.gameObject;
			trip.SetNavTarget (p.transform.position);
		}

		trip.weaponSystem.ShootLasers ();
	}


	public override float EvaluatePriority ()
	{
		float val = (trip.enemiesAround.Count > 0) ? LASER_ATTACK_PRIORITY : 0f;

		val *= (trip.weaponSystem.isLaserAttackReady ()) ? 1f : 0f;

		return val;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripodCloseRange : IABehavior
{
	public static float CLOSE_RANGE_VALUE = 25f;

	TripodController trip;

	public TripodCloseRange (IA ia) : base (ia)
	{
		trip = (TripodController)ia;
	}

	public override void Run ()
	{
		trip.weaponSystem.CloseRangeAttack ();
	}

	public override float EvaluatePriority ()
	{
		float val = 0;


		if (ia.closestEnemy != null) {

			float dist = Vector3.Distance (ia.transform.position, ia.closestEnemy.transform.position);
			//Debug.Log (dist);
			if (dist < trip.weaponSystem.cRDetector) {
				val = CLOSE_RANGE_VALUE;
			}

		}

		val *= (trip.weaponSystem.isCloseRangeReady ()) ? 1f : 0f;

		Debug.Log (val);

		return val;
	}
}

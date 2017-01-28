using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TripodExploration : IABehavior
{

	public static float MIN_DEST_DIST = 5f;
	public static float MAX_DEST_TIME = 7f;

	float lastUpdate;

	float destinationRadius = 50f;

	TripodController trip;

	public TripodExploration (IA ia) : base (ia)
	{
		trip = (TripodController)ia;
	}

	public override void Run ()
	{
		if (Time.time - lastUpdate > MAX_DEST_TIME || (ia.nav.remainingDistance < 5f && Time.time - lastUpdate > MAX_DEST_TIME / 3f)) {
			ia.SetNewRandomDestination ();
			lastUpdate = Time.time;
	
		}
	}

	public virtual void Setup ()
	{
		trip.weaponSystem.target = null;
	}



	public override float EvaluatePriority ()
	{
		return 1f; //Default behavior
	}
}

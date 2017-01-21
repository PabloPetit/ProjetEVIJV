using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TripodExploration : IABehavior
{

	public static float MIN_DEST_DIST = 5f;
	public static float MAX_DEST_TIME = 5f;

	float lastUpdate;

	float destinationRadius = 50f;


	public void Start ()
	{
		
	}

	public TripodExploration (IA ia) : base (ia)
	{
		if (ia.nav.remainingDistance < 5f || Time.time - lastUpdate > MAX_DEST_TIME) {
			ia.SetNewRandomDestination ();
		}
	}




	public override float EvaluatePriority ()
	{
		return 1f; //Default behavior
	}
}

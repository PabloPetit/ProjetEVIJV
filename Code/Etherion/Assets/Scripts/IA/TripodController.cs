using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripodController : IA
{

	public override void SetDesires ()
	{
		

	}

	public override void SetBehaviors ()
	{
		behaviors.Add (new TripodCloseRange (this));
		behaviors.Add (new TripodLaserAttack (this));
		behaviors.Add (new TripodSubAmmoAttack (this));
		behaviors.Add (new TripodExploration (this));
	}

	public override void SetHeadAndBarrel ()
	{
		head = transform.Find ("hips/spine/head/HeadRaycast").gameObject;
		barrel = transform.Find ("hips/spine/head/Barrel").gameObject;
	}
}

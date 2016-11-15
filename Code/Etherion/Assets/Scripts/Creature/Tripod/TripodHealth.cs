using UnityEngine;
using System.Collections;

public class TripodHealth : Health
{

	GameObject tripod;
	Animator anim;

	float destroyDelay = 4f;

	public void Start ()
	{
		base.Start ();
		tripod = transform.parent.gameObject;
		anim = transform.parent.GetComponent<Animator> ();
	}

	public override void Death ()
	{
		
		anim.SetTrigger ("Death");
		foreach (Transform child in tripod.transform) {
			Destroy (child.gameObject, destroyDelay);
		}
		Destroy (tripod, destroyDelay);
	}
}

using UnityEngine;
using System.Collections;

public class MonsterHealth : Health
{

	GameObject monster;
	Animator anim;

	float destroyDelay = 4f;

	public void Start ()
	{
		base.Start ();
		monster = transform.parent.gameObject;
		anim = transform.parent.GetComponent<Animator> ();
	}

	public override void Death ()
	{
		anim.SetTrigger ("Die");
		foreach (Transform child in monster.transform) {
			Destroy (child.gameObject, destroyDelay);
		}
		Destroy (monster, destroyDelay);
	}
}
using UnityEngine;
using System.Collections;

public class AutoGuidedBullet : Bullet
{

	protected GameObject target;
	protected float autoGuidanceStart;
	protected float maxDeviation;



	public static GameObject Create (GameObject prefab, Transform barrel, float speed, float dispertion,
	                                 float initialDamage, float minDamage, float damageDecrease, bool friendlyFire, Player shooter, GameObject target, float autoGuidanceStart, float maxDeviation)
	{

		GameObject pro = Bullet.Create (prefab, barrel, speed, dispertion, initialDamage, minDamage, damageDecrease, friendlyFire, shooter);
		AutoGuidedBullet bullet = pro.GetComponent<AutoGuidedBullet> ();

		bullet.target = target;
		bullet.autoGuidanceStart = autoGuidanceStart;
		bullet.maxDeviation = maxDeviation;

		return pro;
	}

	protected override void FixedUpdate ()
	{
		if (timer > autoGuidanceStart) {
			Vector3 direction = (target.transform.position - transform.position);
			direction = Vector3.RotateTowards (transform.forward, direction, maxDeviation * Time.fixedDeltaTime, 0.0f);
			transform.rotation = Quaternion.LookRotation (direction);
		}
		base.FixedUpdate ();
	}

}

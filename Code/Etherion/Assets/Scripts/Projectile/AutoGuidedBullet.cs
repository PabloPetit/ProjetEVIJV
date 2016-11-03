using UnityEngine;
using System.Collections;

public class AutoGuidedBullet : SimpleBullet {

	protected GameObject target;
	protected float autoGuidanceStart;
	protected float maxDeviation;


	public static GameObject Create(GameObject owner, GameObject prefab, Transform barrel, float speed, float range, float dispertion, int side, float initialDamage, float damageDecrease, float minDamage,
			GameObject target, float autoGuidanceStart, float maxDeviation){

		GameObject projectile = SimpleBullet.Create (owner, prefab, barrel, speed, range, dispertion, side, initialDamage, damageDecrease, minDamage);
		AutoGuidedBullet bullet = projectile.GetComponent<AutoGuidedBullet> ();

		bullet.target = target;
		bullet.autoGuidanceStart = autoGuidanceStart;
		bullet.maxDeviation = maxDeviation;
		return projectile;
	}


	protected override void FixedUpdate (){
		if (timer > autoGuidanceStart) {
			Vector3 direction = (target.transform.position - transform.position);
			direction = Vector3.RotateTowards (transform.forward, direction, maxDeviation * Time.fixedDeltaTime, 0.0f);
			transform.rotation = Quaternion.LookRotation (direction);
		}
		base.FixedUpdate ();
	}

}

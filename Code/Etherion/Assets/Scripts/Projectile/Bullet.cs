using UnityEngine;
using System.Collections;

public class Bullet : Projectile, IDamage {


	public static GameObject Create (GameObject prefab, Transform barrel, float speed, float dispertion,
		float initialDamage, float minDamage, float damageDecrease, bool friendlyFire, bool hitMarker, GameObject shooter)
	{
		GameObject pro = Projectile.Create (prefab, barrel, speed, dispertion);


	}

}

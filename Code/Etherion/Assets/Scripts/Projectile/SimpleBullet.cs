using UnityEngine;
using System.Collections;

public class SimpleBullet : Projectile {

	protected int side;
	protected float initialDamage;
	protected float damageDecrease;
	protected float minDamage;


	public static GameObject Create(GameObject owner, GameObject prefab, Transform barrel, float speed, float range, float dispertion, int side, float initialDamage, float damageDecrease, float minDamage,bool hitMarker = false){
		GameObject projectile = Projectile.Create (owner, prefab, barrel,speed);
		SimpleBullet bullet = projectile.GetComponent<SimpleBullet> ();
		projectile.transform.Rotate (new Vector3(Random.Range (-dispertion, dispertion),Random.Range (-dispertion, dispertion),Random.Range (-dispertion, dispertion)));
		bullet.side = side;
		bullet.initialDamage = initialDamage;
		bullet.damageDecrease = damageDecrease;
		bullet.minDamage = minDamage;

		return projectile;
	}

	protected override void OnTriggerEnter(Collider other){
		SimpleDamage (other);
	}


	protected void SimpleDamage(Collider other){

		bool shot = false;

		float damage = Mathf.Max (minDamage,initialDamage - damageDecrease * timer);

		if (other.gameObject == owner) {
			return;
		}


	
		Delete ();
	}

}

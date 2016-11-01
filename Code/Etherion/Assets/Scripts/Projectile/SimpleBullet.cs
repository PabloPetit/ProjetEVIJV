using UnityEngine;
using System.Collections;

public class SimpleBullet : Projectile {

	protected int side;
	protected float initialDamage;
	protected float damageDecrease;
	protected float minDamage;

	protected HitMarker hitMarkerUI;
	protected bool hitMarker;


	public static GameObject Create(GameObject owner, GameObject prefab, Transform barrel, float speed, float range, float dispertion, int side, float initialDamage, float damageDecrease, float minDamage,bool hitMarker = false){
		GameObject projectile = Projectile.Create (owner, prefab, barrel,speed,range);
		SimpleBullet bullet = projectile.GetComponent<SimpleBullet> ();
		projectile.transform.Rotate (new Vector3(Random.Range (-dispertion, dispertion),Random.Range (-dispertion, dispertion),Random.Range (-dispertion, dispertion)));
		bullet.side = side;
		bullet.initialDamage = initialDamage;
		bullet.damageDecrease = damageDecrease;
		bullet.minDamage = minDamage;
		bullet.hitMarker = hitMarker;

		if (bullet.hitMarker) {
			bullet.hitMarkerUI = GameObject.Find ("HitMarker").GetComponent<HitMarker> ();
		}

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

		if (other.tag.Equals ("Player")) {
			PlayerState state = other.GetComponent<PlayerState> ();
			if (state.side != side){
				PlayerHealth health = other.GetComponent<PlayerHealth> ();
				if (health != null && !health.dead) {
					health.TakeDamage (damage,side);
					shot = true;
				}
			}
		}else if (other.tag.Equals ("Creature")){
			CreatureHealth health = other.GetComponent<CreatureHealth> ();
			if (health != null && !health.dead) {
				health.TakeDamage (damage, owner);
				shot = true;
			}
		}

		if (shot && hitMarker) {
			hitMarkerUI.hit ();
		}

		Delete ();
	}

}

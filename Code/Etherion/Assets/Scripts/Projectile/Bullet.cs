using UnityEngine;
using System.Collections;

public class Bullet : Projectile, IDamage
{
	public Player shooter { get ; set ; }

	public bool hitMarker { get ; set ; }

	public bool friendlyFire { get ; set ; }

	public float initialDamage { get ; set ; }

	public float minDamage { get ; set ; }

	public float damageDecrease { get ; set ; }



	public static GameObject Create (GameObject prefab, Transform barrel, float speed, float dispertion,
	                                 float initialDamage, float minDamage, float damageDecrease, bool friendlyFire, bool hitMarker, Player shooter)
	{
		GameObject pro = Projectile.Create (prefab, barrel, speed, dispertion);
		Bullet bullet = pro.GetComponent<Bullet> ();
	
		bullet.initialDamage = initialDamage;
		bullet.minDamage = minDamage;
		bullet.damageDecrease = damageDecrease;
		bullet.friendlyFire = friendlyFire;
		bullet.hitMarker = hitMarker;
		bullet.shooter = shooter;

		return pro;
	}

	protected virtual void OnTriggerEnter (Collider other)
	{

		GameObject go = other.gameObject;

		Player target = go.GetComponent<Player> ();


		if (target != null) {

			if (target.id == shooter.id) {
				return;
			}

			float damages = Mathf.Max (minDamage, initialDamage - (damageDecrease * timer));

			Damage.DoDamage (shooter, target, damages, friendlyFire, hitMarker);
		}

		Delete ();
	}

}

using UnityEngine;
using System.Collections;

public class SkratWeapon : PlayerWeapon
{

	public GameObject bulletPrefab;
	AudioSource gunShot;

	void Start ()
	{
		gunShot = barrel.GetComponent<AudioSource> ();
	}

	protected override void Action ()
	{
		//	SimpleBullet.Create (gameObject, bulletPrefab, barrel.transform, speed, range, dispertion, player, damagePerShot, damageDecrease, minDamage,hitMarker:true);
		Bullet.Create (bulletPrefab, barrel.transform, speed, dispertion, initialDamage, minDamage, damageDecrease, false, player);
		gunShot.Play ();
	}

	public override void EnableEffects (Vector3 start, Vector3 end)
	{
		
	}

	public override void DisableEffects ()
	{
	}
}

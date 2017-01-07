using UnityEngine;
using System.Collections;

public class SkratWeapon : PlayerWeapon
{

	public static float detonationVolume = 0.75f;

	public GameObject bulletPrefab;
	AudioSource gunShot;

	void Start ()
	{
		gunShot = barrel.GetComponent<AudioSource> ();
		gunShot.volume = detonationVolume;
	}

	protected override void Action ()
	{
		
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

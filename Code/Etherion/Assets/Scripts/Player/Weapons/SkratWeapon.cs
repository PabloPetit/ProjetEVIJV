using UnityEngine;
using System.Collections;

public class SkratWeapon : PlayerWeapon
{

	public static float detonationVolume = 0.75f;

	public GameObject bulletPrefab;
	AudioSource gunShot;

	public GameObject effects;

	void Start ()
	{
		gunShot = barrel.GetComponent<AudioSource> ();
		gunShot.volume = detonationVolume;
	}

	protected override void Action ()
	{
		
		Bullet.Create (bulletPrefab, barrel.transform, speed, dispertion, initialDamage, minDamage, damageDecrease, false, player);
		gunShot.Play ();
		/*
		GameObject effect = Instantiate (effects);
		effect.transform.position = barrel.transform.position;
		effect.transform.rotation = barrel.transform.rotation;
		*/
	}

	public override void EnableEffects (Vector3 start, Vector3 end)
	{
		
	}

	public override void DisableEffects ()
	{
	}
}

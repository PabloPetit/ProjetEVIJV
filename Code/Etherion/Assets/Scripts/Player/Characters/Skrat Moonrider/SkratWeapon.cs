using UnityEngine;
using System.Collections;

public class SkratWeapon : PlayerWeapon{

	public GameObject bulletPrefab;
	AudioSource gunShot;


	/*
	 * 
	 * 
	 * 
	 * 
	 */


	void Start () {
		gunShot = barrel.GetComponent<AudioSource> ();
	}

	protected override void Action(){
		Projectile.Create (gameObject, bulletPrefab, barrel.transform, dispertion, playerState.side, damagePerShot, damageDecrease, speed, range); 
		gunShot.Play ();
	}
		
	public override void EnableEffects(Vector3 start, Vector3 end){
		
	}
	public override void DisableEffects (){
	}
}

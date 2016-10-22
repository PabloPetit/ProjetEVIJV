using UnityEngine;
using System.Collections;

public class SkratWeapon : PlayerWeapon{


	// No update for weapon effects for now ...
	public GameObject bulletPrefab;
	AudioSource gunShot;

	void Start () {
		damagePerShot = 2f;
		timeBetweenBullets = 0.15f;
		range = 1000f;
		effectsDisplayTime = 0.30f;
		overloadTime = 0f;
		overLoaded = false;
		timer = 0f;
		//gunShot = transform.Find ("Head/BarrelEnd").gameObject.GetComponent<AudioSource> ();
	}

	public override void Action(){

		GameObject proj = (GameObject) Instantiate (bulletPrefab, barrel.transform.position,barrel.transform.rotation);
		Projectile p = proj.GetComponent<Projectile> ();
		p.side = 1; // A changer
	}


	public override void EnableEffects(Vector3 start, Vector3 end){
		//gunShot.Play ();
	}
	public override void DisableEffects (){
	}
}

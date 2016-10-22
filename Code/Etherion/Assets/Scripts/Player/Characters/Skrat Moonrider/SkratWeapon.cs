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
		dispertion = .1f;
		effectsDisplayTime = 0.30f;
		overloadTime = 0f;
		overLoaded = false;
		timer = 0f;


		gunShot = barrel.GetComponent<AudioSource> ();
	}

	public override void Action(){

		GameObject proj = (GameObject) Instantiate (bulletPrefab, barrel.transform.position,barrel.transform.rotation);
		proj.transform.Rotate (new Vector3(Random.Range (-dispertion, dispertion),Random.Range (-dispertion, dispertion),Random.Range (-dispertion, dispertion)));
		Projectile p = proj.GetComponent<Projectile> ();
		p.side = 1; // A changer
		//gunShot.Play ();
		//p.range = range;
	}


	public override void EnableEffects(Vector3 start, Vector3 end){
		
	}
	public override void DisableEffects (){
	}
}

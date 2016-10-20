using UnityEngine;
using System.Collections;

public class SkratWeapon : PlayerWeapon{


	// No update for weapon effects for now ...

	GameObject gunLine;
	AudioSource gunShot;

	void Start () {
		damagePerShot = 2f;
		timeBetweenBullets = 0.33f;
		range = 1000f;
		effectsDisplayTime = 0.30f;
		overloadTime = 0f;
		overLoaded = false;
		timer = 0f;
		//gunLine = transform.Find ("Head/BarrelEnd/VolumetricLinePrefab").gameObject;
		//gunShot = transform.Find ("Head/BarrelEnd").gameObject.GetComponent<AudioSource> ();
	}


	public override void EnableEffects(Vector3 start, Vector3 end){
		//gunLine.SetActive (true);
		//gunShot.Play ();
	}
	public override void DisableEffects (){
		//gunLine.SetActive (false);
	}
}

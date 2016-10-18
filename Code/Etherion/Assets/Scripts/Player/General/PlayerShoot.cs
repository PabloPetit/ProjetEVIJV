using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	float timer;
	Ray shootRay;
	RaycastHit shootHit;

	int enemyMask;
	int creatureMask;
	int environementMask;

	Camera camera;
	GameObject barrel;

	PlayerWeapon weapon;

	void Awake () {
		
		enemyMask = LayerMask.GetMask ("Enemies");
		creatureMask = LayerMask.GetMask ("Creatures");
		environementMask = LayerMask.GetMask ("Environement");

		camera = GetComponent <Camera> ();
		barrel = transform.Find("Head/BarrelEnd").gameObject;
	

		weapon = GetComponent<PlayerWeapon> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if(timer >= weapon.timeBetweenBullets * weapon.effectsDisplayTime)
		{
			weapon.DisableEffects ();
		}
	}
		

	public void Shoot (){

		// TODO: Overload
		if (timer >= weapon.timeBetweenBullets && Time.timeScale != 0) { 

			timer = 0f;

			shootRay.origin = barrel.transform.position;
			shootRay.direction = barrel.transform.forward;

			if (Physics.Raycast (shootRay, out shootHit, weapon.range, enemyMask)) {
				// We shot an enemy
				PlayerHealth enemyHealth = shootHit.collider.GetComponent<PlayerHealth> ();
				//Do Damage !
				weapon.EnableEffects (shootRay.origin,shootHit.point);
			} else if (Physics.Raycast (shootRay, out shootHit, weapon.range, creatureMask)) {
				//Do the same but to litle creatures
				weapon.EnableEffects (shootRay.origin,shootHit.point);
			} else {
				// If it didn't hit anything
				weapon.EnableEffects (shootRay.origin, shootRay.origin + shootRay.direction * weapon.range);
			}
		}
	}
}

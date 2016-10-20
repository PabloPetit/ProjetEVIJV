using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

	public float damagePerShot;
	public float timeBetweenBullets;
	public float range;
	public float effectsDisplayTime;
	public float overloadTime;

	protected bool overLoaded;
	protected float timer;
	protected GameObject barrel;

	Ray shootRay;
	RaycastHit shootHit;

	int enemyMask;
	int creatureMask;
	int environementMask;




	void Awake () {

		enemyMask = LayerMask.GetMask ("Enemies");
		creatureMask = LayerMask.GetMask ("Creatures");
		environementMask = LayerMask.GetMask ("Environement");
		barrel = transform.Find("Model/Head/RightHand").gameObject;

	}

	void Update () {
		timer += Time.deltaTime;

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}
	}


	public void Shoot (){

		// TODO: Overload
		if (timer >= timeBetweenBullets && Time.timeScale != 0) { 

			timer = 0f;

			shootRay.origin = barrel.transform.position;
			shootRay.direction = barrel.transform.forward;

			if (Physics.Raycast (shootRay, out shootHit, range, enemyMask)) {
				// We shot an enemy
				PlayerHealth enemyHealth = shootHit.collider.GetComponent<PlayerHealth> ();
				//Do Damage !
				EnableEffects (shootRay.origin,shootHit.point);
			} else if (Physics.Raycast (shootRay, out shootHit, range, creatureMask)) {
				//Do the same but to litle creatures
				EnableEffects (shootRay.origin,shootHit.point);
			} else {
				// If it didn't hit anything
				EnableEffects (shootRay.origin, shootRay.origin + shootRay.direction * range);
			}
		}
	}



	public virtual void EnableEffects(Vector3 start, Vector3 end){
	}
	public virtual void DisableEffects (){
	}

}

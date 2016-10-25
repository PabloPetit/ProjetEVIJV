using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

	public float damagePerShot;
	public float range;
	public float dispertion;
	public float speed;
	public float damageDecrease;
	public float overloadTime;
	public float timeBetweenBullets;
	public float effectsDisplayTime;


	protected bool overLoaded;
	protected float timer;
	protected GameObject barrel;
	protected PlayerState playerState;

	Ray shootRay;
	RaycastHit shootHit;

	int playerMask;
	int creatureMask;
	int environementMask;

	/*
	 * 
	 * TEST ZONE
	 * 
	 */

	float recoilForce = .1f;
	float recoilTime = 0.4f;
	float maxDeviation = 01f;

	Vector3 recoilTarget;

	Camera camera;

	GameObject rightHand;

	void Recoil(){

		if (timer < recoilTime) {
			
		} else {
			
		}

	}

	void Awake () {
		recoilTarget  = new Vector3(-maxDeviation,0f,0f);
		playerMask = LayerMask.GetMask ("Player");
		creatureMask = LayerMask.GetMask ("Creatures");
		environementMask = LayerMask.GetMask ("Environement");
		barrel = transform.Find("Model/Head/RightHand/Gun/BarrelEnd").gameObject;
		playerState = GetComponent<PlayerState> ();
		camera = transform.Find ("Model/Head").gameObject.GetComponent<Camera> ();
		rightHand = transform.Find ("Model/Head/RightHand").gameObject;
	}

	void Update () {
		timer += Time.deltaTime;

		Recoil ();

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}
	}


	public void Shoot (){

		// TODO: Overload
		if (timer >= timeBetweenBullets && Time.timeScale != 0) { 

			timer = 0f;

			EnableEffects(barrel.transform.position,barrel.transform.forward);

			Action ();


			/*

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
			*/
		}
	}



	protected virtual void Action(){
		
	}

	public virtual void EnableEffects(Vector3 start, Vector3 direction){
	}

	public virtual void DisableEffects (){
	}

}

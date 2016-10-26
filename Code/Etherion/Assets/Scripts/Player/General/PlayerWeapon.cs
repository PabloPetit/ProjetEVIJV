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
	 * RECOIL
	 * 
	 */

	Camera camera;
	GameObject rightHand;

	// Force and time parameters
	float recoilForce = 5f;
	float counterRecoilForce = 5f;
	float recoilTime = 0.1f;
	float downwardTime = .3f;
	float downwardRecoverForce = 3.8f;


	// Recoil on Hand rotation
	float maxDeviationX = 5f;
	float maxDeviationY = 2f;
	float maxDeviationZ = 1.5f;
	Vector3 recoilTarget;

	// Recoil on Hand position
	float backWardForce = .35f;
	float maxBackWardDeviation = 2f;

	// Downward position
	Vector3 downwardPosition;
	float downwardDeviationX = -1.5f;



	void Awake () {
		playerMask = LayerMask.GetMask ("Player");
		creatureMask = LayerMask.GetMask ("Creatures");
		environementMask = LayerMask.GetMask ("Environement");
		barrel = transform.Find("Model/Head/RightHand/Gun/BarrelEnd").gameObject;
		playerState = GetComponent<PlayerState> ();
		camera = transform.Find ("Model/Head").gameObject.GetComponent<Camera> ();
		rightHand = transform.Find ("Model/Head/RightHand").gameObject;

		recoilTarget  = new Vector3(-maxDeviationX,maxDeviationY,maxDeviationZ);
		downwardPosition  = new Vector3(-downwardDeviationX,0f,0f);
	}

	void Update () {
		timer += Time.deltaTime;

		Recoil ();

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}
	}

	void Recoil(){

		if (timer < recoilTime) {
			rightHand.transform.localRotation = Quaternion.Lerp(rightHand.transform.localRotation, Quaternion.Euler (recoilTarget), recoilForce*Time.deltaTime);
			rightHand.transform.localPosition = Vector3.Lerp (rightHand.transform.localPosition, Vector3.back * maxBackWardDeviation, backWardForce * Time.deltaTime);

		}else if(timer < recoilTime + downwardTime){
			rightHand.transform.localRotation = Quaternion.Lerp(rightHand.transform.localRotation, Quaternion.Euler (downwardPosition), counterRecoilForce*Time.deltaTime);
			rightHand.transform.localPosition = Vector3.Lerp (rightHand.transform.localPosition, Vector3.zero, backWardForce * Time.deltaTime);
		}
		else {
			rightHand.transform.localRotation = Quaternion.Lerp(rightHand.transform.localRotation, Quaternion.Euler (Vector3.zero), downwardRecoverForce*Time.deltaTime);
			rightHand.transform.localPosition = Vector3.Lerp (rightHand.transform.localPosition, Vector3.zero, backWardForce * Time.deltaTime);
		}

	}


	public void Shoot (){

		// TODO: Overload
		if (timer >= timeBetweenBullets && Time.timeScale != 0) { 

			timer = 0f;

			EnableEffects(barrel.transform.position,barrel.transform.forward);

			Action ();

			recoilTarget.y = (Random.Range (-maxDeviationY, maxDeviationY));

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

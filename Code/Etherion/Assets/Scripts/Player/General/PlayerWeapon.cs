using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour {


	// Weapon Specs
	public float damagePerShot;
	public float range;
	public float dispertion;
	public float speed;
	public float damageDecrease;
	public float minDamage;

	//Effetcs

	public float timeBetweenBullets;
	public float effectsDisplayTime;

	// Misc Data
	protected float timer;
	protected GameObject barrel;
	protected PlayerState playerState;
	protected HumanAim humanAim;
	protected Image overloadBar;
	protected float overloadBarMax = .1f;
	protected float overloadBarMin = .9f;

	int playerMask;
	int creatureMask;
	int environementMask;

	//OverLoad
	protected bool overLoaded;
	public float overloadIncrement;
	public float overloadThreshold;
	public float overloadVal;
	public float overloadCoolSpeed;

	Ray shootRay;
	RaycastHit shootHit;



	// Recoil

	Camera camera;
	GameObject rightHand;

	// Force and time parameters
	float recoilForce = 2f;
	float counterRecoilForce = 5f;
	float recoilTime = 0.1f;
	float downwardTime = .3f;
	float downwardRecoverForce = 3.8f;

	float aimReducer = 1.5f;


	// Recoil on Hand rotation
	float maxDeviationX = 5f;
	float maxDeviationY = 1f;
	float maxDeviationZ = 0f;
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
		humanAim = GetComponent<HumanAim> ();
		camera = transform.Find ("Model/Head").gameObject.GetComponent<Camera> ();
		rightHand = transform.Find ("Model/Head/RightHand").gameObject;

		overloadBar = GameObject.Find ("OverloadBar").GetComponent<Image> ();

		recoilTarget  = new Vector3(-maxDeviationX,maxDeviationY,maxDeviationZ);
		downwardPosition  = new Vector3(-downwardDeviationX,0f,0f);

		overloadVal = 0f;
		overLoaded = false;
	}

	void Update () {
		timer += Time.deltaTime;

		Recoil ();

		DecreaseOverload ();

		setOverloadBarValue ();

		if(timer >= timeBetweenBullets * effectsDisplayTime)
		{
			DisableEffects ();
		}
	}

	void setOverloadBarValue(){

		float val = overloadVal / overloadThreshold;
		val *= (overloadBarMax - overloadBarMin);
		val += overloadBarMin;
		overloadBar.fillAmount = 1f - val;

		if (overLoaded) {
			overloadBar.color = Color.red;
		} else {
			overloadBar.color = Color.white;
		}
	}

	void Recoil(){

		if (timer < recoilTime) {
			rightHand.transform.localRotation = Quaternion.Lerp(rightHand.transform.localRotation,
				Quaternion.Euler (recoilTarget), (humanAim.aiming ? recoilForce / aimReducer : recoilForce)*Time.deltaTime);
			rightHand.transform.localPosition = Vector3.Lerp (rightHand.transform.localPosition, Vector3.back * maxBackWardDeviation,
				(humanAim.aiming ? backWardForce / aimReducer : recoilForce) * Time.deltaTime);

		}else if(timer < recoilTime + downwardTime){
			rightHand.transform.localRotation = Quaternion.Lerp(rightHand.transform.localRotation, Quaternion.Euler (downwardPosition),
				(humanAim.aiming ? counterRecoilForce / aimReducer : counterRecoilForce)*Time.deltaTime);
			rightHand.transform.localPosition = Vector3.Lerp (rightHand.transform.localPosition, Vector3.zero, backWardForce * Time.deltaTime);
		}
		else {
			rightHand.transform.localRotation = Quaternion.Lerp(rightHand.transform.localRotation, Quaternion.Euler (Vector3.zero), downwardRecoverForce*Time.deltaTime);
			rightHand.transform.localPosition = Vector3.Lerp (rightHand.transform.localPosition, Vector3.zero, backWardForce * Time.deltaTime);
		}

	}


	public void Shoot (){

		// TODO: Overload
		if (timer >= timeBetweenBullets && Time.timeScale != 0  && !overLoaded) { 

			timer = 0f;

			EnableEffects(barrel.transform.position,barrel.transform.forward);

			Action ();

			recoilTarget.y = (Random.Range (-maxDeviationY, maxDeviationY));

			IncreaseOverload ();

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

	protected void DecreaseOverload(){
		overloadVal = Mathf.Max (0f, overloadVal - overloadCoolSpeed * Time.deltaTime);
		if (overLoaded && overloadVal == 0f) {
			overLoaded = false;
		}

	}

	protected void IncreaseOverload(){
		overloadVal += overloadIncrement;
		if (overloadVal > overloadThreshold) {
			overLoaded = true;
		}
	}


	protected virtual void Action(){
		
	}

	public virtual void EnableEffects(Vector3 start, Vector3 direction){
	}

	public virtual void DisableEffects (){
	}

}

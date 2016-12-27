using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerWeapon : MonoBehaviour
{
	// Weapon Specs
	public float initialDamage;
	public float range;
	public float dispertion;
	public float speed;
	public float damageDecrease;
	public float minDamage;


	public float timeBetweenBullets;

	public float effectsDisplayTime;

	// Misc Data
	protected float timer;
	protected GameObject barrel;
	protected Player player;
	protected HumanAim humanAim;
	protected Image overloadBar;
	protected float overloadBarMax = .1f;
	protected float overloadBarMin = .9f;



	EventName overloadEvent;

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
	float maxDeviationX = 4f;
	float maxDeviationY = 1.5f;
	float maxDeviationZ = 0f;
	Vector3 recoilTarget;

	// Recoil on Hand position
	float backWardForce = .35f;
	float maxBackWardDeviation = 1.25f;

	// Downward position
	Vector3 downwardPosition;
	float downwardDeviationX = -1f;


	void Awake ()
	{
		barrel = transform.Find ("Model/Head/RightHand/Gun/BarrelEnd").gameObject;
		player = GetComponent<Player> ();
		humanAim = GetComponent<HumanAim> ();
		camera = transform.Find ("Model/Head").gameObject.GetComponent<Camera> ();
		rightHand = transform.Find ("Model/Head/RightHand").gameObject;


		recoilTarget = new Vector3 (-maxDeviationX, maxDeviationY, maxDeviationZ);
		downwardPosition = new Vector3 (-downwardDeviationX, 0f, 0f);

		overloadVal = 0f;
		overLoaded = false;
		overloadEvent = new EventName (OverloadBar.OVERLOAD_BAR_CHANNEL);
	}

	void Update ()
	{
		timer += Time.deltaTime;

		Recoil ();

		DecreaseOverload ();

		setOverloadBarValue ();

		if (timer >= timeBetweenBullets * effectsDisplayTime) {
			DisableEffects ();
		}
	}

	void setOverloadBarValue ()
	{
		float val = overloadVal / overloadThreshold;
		val *= (overloadBarMax - overloadBarMin);
		val += overloadBarMin;
		val = 1f - val;
		EventManager.TriggerAction (overloadEvent, new object[]{ val, overLoaded });

	}

	void Recoil ()
	{

		if (timer < recoilTime) {
			rightHand.transform.localRotation = Quaternion.Lerp (rightHand.transform.localRotation,
				Quaternion.Euler (recoilTarget), (humanAim.aiming ? recoilForce / aimReducer : recoilForce) * Time.deltaTime);
			rightHand.transform.localPosition = Vector3.Lerp (rightHand.transform.localPosition, Vector3.back * maxBackWardDeviation,
				(humanAim.aiming ? backWardForce / aimReducer : recoilForce) * Time.deltaTime);

		} else if (timer < recoilTime + downwardTime) {
			rightHand.transform.localRotation = Quaternion.Lerp (rightHand.transform.localRotation, Quaternion.Euler (downwardPosition),
				(humanAim.aiming ? counterRecoilForce / aimReducer : counterRecoilForce) * Time.deltaTime);
			rightHand.transform.localPosition = Vector3.Lerp (rightHand.transform.localPosition, Vector3.zero, backWardForce * Time.deltaTime);
		} else {
			rightHand.transform.localRotation = Quaternion.Lerp (rightHand.transform.localRotation, Quaternion.Euler (Vector3.zero), downwardRecoverForce * Time.deltaTime);
			rightHand.transform.localPosition = Vector3.Lerp (rightHand.transform.localPosition, Vector3.zero, backWardForce * Time.deltaTime);
		}

	}


	public void Shoot ()
	{


		if (timer >= timeBetweenBullets && Time.timeScale != 0 && !overLoaded) { 

			timer = 0f;

			EnableEffects (barrel.transform.position, barrel.transform.forward);

			Action ();

			recoilTarget.y = (Random.Range (-maxDeviationY, maxDeviationY));

			IncreaseOverload ();

		}
	}

	protected void DecreaseOverload ()
	{
		overloadVal = Mathf.Max (0f, overloadVal - overloadCoolSpeed * Time.deltaTime);
		if (overLoaded && overloadVal == 0f) {
			overLoaded = false;
		}

	}

	protected void IncreaseOverload ()
	{
		overloadVal += overloadIncrement;
		if (overloadVal > overloadThreshold) {
			overLoaded = true;
		}
	}


	protected virtual void Action ()
	{
		
	}

	public virtual void EnableEffects (Vector3 start, Vector3 direction)
	{
	}

	public virtual void DisableEffects ()
	{
	}

}

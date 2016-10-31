using UnityEngine;
using System.Collections;

public class TripodController : MonoBehaviour {

	// Tripod States

	public const int EXPLORATION = 1;
	public const int AQUISITION = 2;
	public const int FIRE = 3;

	// Misc data

	NavMeshAgent nav;
	TripodHealth health;
	Animator anim;
	AudioSource audio;


	int playerMask;
	int environnementMask;

	// Movement : 
	public float speed;
	public float angularSpeed;
	public float destinationRadius;
	Vector3 navDestination;

	// FootSteps
	public AudioClip[] footsteps;
	int lastFootstep;
	float footstepsInterval = 1f;

	// Aiming and Firing

	Ray shootRay;
	RaycastHit shootHit;

	public GameObject target;
	public float detectionRadius;
	public float visionAngle;
	public float maxAimingDistance;
	public float aquisitionTime;
	public float loseTargetTime;

	Light chargingLight;

	float timer;
	bool acquiring;
	int state;


	float firstFire = 0.6f;
	float secondFire = 0.75f;
	float fireAnimationDuration = 1.75f;
	int shots;

	public float balisticShotAngle;
	public float balisticShotMinDistance;

	// Weapon Specs

	public GameObject bulletPrefab;
	GameObject barrel;

	public float damage;
	public float damageDecrease;
	public float bulletSpeed;
	public float maxDeviation;
	public float range;
	public float autoGuidanceStart;


	void Start () {
		playerMask = LayerMask.GetMask ("Player");
		environnementMask = LayerMask.GetMask ("Environement");
		nav = GetComponent<NavMeshAgent> ();
		health = transform.Find ("hips").GetComponent<TripodHealth> ();
		barrel = transform.Find ("hips/spine/head/Barrel").gameObject;
		anim = transform.GetComponent<Animator> ();
		audio = GetComponent<AudioSource> ();
		chargingLight = transform.Find ("hips/ChargingLight").gameObject.GetComponent<Light> ();
		target = null;
		acquiring = false;
		navDestination = Vector3.zero;
		GoToExploration ();
		lastFootstep = 0;
	}
	

	void Update () {
		//Debug.Log (state);
		timer += Time.deltaTime;
		if (!health.dead) {
			stateSwitch ();
		}
	}

	void stateSwitch(){

		switch (state) {
		case EXPLORATION:
			Exploration ();
			break;
		case AQUISITION:
			Aquisition ();
			break;
		case FIRE: 
			Fire ();
			break;
		}
	}

	void Exploration(){
		
		setTarget ();

		if (target != null) {
			GoToAquisition ();
			return;
		}

		if (Vector3.Distance (nav.transform.position, navDestination) < 1) {
			navDestination = Vector3.zero;
		}

		if ( navDestination == Vector3.zero ){
			SetNewDestination ();
			nav.SetDestination (navDestination);
		}

		footStepSound ();

		Vector3 direction = (nav.destination - transform.position);//.normalized;
		direction = Vector3.RotateTowards (transform.forward, direction, angularSpeed * Time.fixedDeltaTime, 0.0f);
		direction.y = 0f;
		transform.rotation = Quaternion.LookRotation (direction);
	}

	void footStepSound(){
		if (timer > footstepsInterval && footsteps.Length > 0) {
			audio.PlayOneShot (footsteps[lastFootstep]);
			lastFootstep++;
			timer = 0f;
			if (lastFootstep == footsteps.Length) {
				lastFootstep = 0;
			}
		}
	}

	void GoToExploration(){
		shots = 0;
		state = EXPLORATION;
		anim.SetTrigger ("Walking");
		timer = 0f;
		nav.Resume ();
	}

	void SetNewDestination(){
		navDestination = Random.insideUnitSphere * destinationRadius;
		navDestination += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition (navDestination,out hit, destinationRadius,1);
		navDestination = hit.position;
	}

	void GoToAquisition(){
		state = AQUISITION;
		timer = 0f;
		anim.SetTrigger ("Idle");
	}

	void Aquisition(){

		nav.Stop ();
		
		checkTargetDistance ();

		if (target == null) {
			chargingLight.intensity = 0f;
			GoToExploration ();
			return;
		}

		Aim ();

		chargingLight.intensity += 8f / aquisitionTime * Time.deltaTime;

		if (timer > aquisitionTime) {
			GoToFire ();
			return;
		}
	}

	void GoToFire(){
		state = FIRE;
		anim.SetTrigger ("Fire");
		timer = 0f;
	}
		
	void Fire(){
		Aim ();
		if (timer > firstFire && shots == 0) {
			ShootTarget ();
		} else if (timer > secondFire && shots == 1) {
			ShootTarget ();
			chargingLight.intensity = 0f;
		} else if (shots == 2 && timer > fireAnimationDuration) {
			GoToExploration ();
		} 
	}

	void ShootTarget(){
		if ( Vector3.Distance (transform.position,target.transform.position) > balisticShotMinDistance && !isTargetVisible (target,maxAimingDistance)) {
			barrel.transform.Rotate (new Vector3 (-balisticShotAngle, 0f, 0f));
			Projectile.Create (transform.Find ("hips").gameObject, bulletPrefab, barrel.transform, 0f, 0, damage, damageDecrease, bulletSpeed, range, true, maxDeviation, target, autoGuidanceStart); 
			barrel.transform.Rotate (new Vector3 (balisticShotAngle, 0f, 0f));
		} else {
			Projectile.Create (transform.Find ("hips").gameObject, bulletPrefab, barrel.transform, 0f, 0, damage, damageDecrease, bulletSpeed, range, true, maxDeviation, target, autoGuidanceStart); 
		}


		audio.Play ();
		shots++;
	}

	void Aim(){
		// Model Aim
		Vector3 direction = (target.transform.position - transform.position);//.normalized;
		direction = Vector3.RotateTowards (transform.forward, direction, angularSpeed * Time.fixedDeltaTime, 0.0f);
		direction.y = 0f;
		transform.rotation = Quaternion.LookRotation (direction);
		//Barrel Aim
		direction = (target.transform.position - barrel.transform.position);//.normalized;
		direction = Vector3.RotateTowards (barrel.transform.forward, direction, angularSpeed * Time.fixedDeltaTime, 0.0f);
		barrel.transform.rotation = Quaternion.LookRotation (direction);

	}

	void checkTargetDistance(){
		// If too far, avoid 
		if (target != null) {
			if (Vector3.Distance (transform.position, target.transform.position) > maxAimingDistance) {
				target = null;
			}
		} 
	}

	void setTarget(){
		// List of all colliders in range detectionRadius
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, detectionRadius, playerMask);
		GameObject closest = null;
		float minDistance = detectionRadius + 1f;
		// For each one of them
		foreach (Collider col in hitColliders) {
			// If it is in the vision cone
			if (Vector3.Angle ( col.gameObject.transform.position - transform.position, transform.forward) < visionAngle) {
				float dist = Vector3.Distance (transform.position, col.gameObject.transform.position);
				// If it is closer than the current best one and it is not behind a wall
				if (dist < minDistance && isTargetVisible (col.gameObject,detectionRadius)) {
					closest = col.gameObject;
					minDistance = dist;
				}
			}
		}
		if (closest != null) {
			target = closest;
		}
	}

	bool isTargetVisible(GameObject rayTarget, float range){
		bool res = false;
		shootRay.origin = transform.position;
		shootRay.direction = (rayTarget.transform.position - transform.position).normalized;

		if (Physics.Raycast (shootRay, out shootHit, maxAimingDistance,playerMask | environnementMask)){
			if (shootHit.transform.gameObject == rayTarget){
				res = true;
			}
		}

		return res;
	}
}

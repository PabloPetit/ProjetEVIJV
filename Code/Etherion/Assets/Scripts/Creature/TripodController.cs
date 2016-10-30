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

	// Movement : 
	public float speed;
	public float angularSpeed;
	public float destinationRadius;
	Vector3 navDestination;

	// Aiming and Firing

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
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (state);
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

		Vector3 direction = (nav.destination - transform.position);//.normalized;
		direction = Vector3.RotateTowards (transform.forward, direction, angularSpeed * Time.fixedDeltaTime, 0.0f);
		direction.y = 0f;
		transform.rotation = Quaternion.LookRotation (direction);
	}

	void GoToExploration(){
		shots = 0;
		state = EXPLORATION;
		anim.SetTrigger ("Walking");
		timer = 0f;
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
			Projectile.Create (gameObject, bulletPrefab, barrel.transform, 0f, 0, damage, damageDecrease, bulletSpeed, range, true, maxDeviation, target, autoGuidanceStart); 
			audio.Play ();
			shots++;
		} else if (timer > secondFire && shots == 1) {
			Projectile.Create (gameObject, bulletPrefab, barrel.transform, 0f, 0, damage, damageDecrease, bulletSpeed, range, true, maxDeviation, target, autoGuidanceStart); 
			audio.Play ();
			chargingLight.intensity = 0f;
			shots++;
		} else if (shots == 2 && timer > fireAnimationDuration) {
			GoToExploration ();
		} 
	}

	void Aim(){
		//TODO : Remonter la viser si raycast echoue
		// Si on est orienter vers la cible avec un delta < seuil mais que pas Raycast, tire en cloche

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
		if (target != null) {
			if (Vector3.Distance (transform.position, target.transform.position) > maxAimingDistance) {
				target = null;
			}
		} 
	}

	void setTarget(){
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, detectionRadius, playerMask);
		GameObject closest = null;
		float minDistance = detectionRadius + 1f;
		foreach (Collider col in hitColliders) {
			if (Vector3.Angle ( col.gameObject.transform.position - transform.position, transform.forward) < visionAngle) {
				float dist = Vector3.Distance (transform.position, col.gameObject.transform.position);
				if (dist < minDistance) {
					closest = col.gameObject;
					minDistance = dist;
				}
			}
		}
		if (closest != null) {
			target = closest;
		}
	}
}

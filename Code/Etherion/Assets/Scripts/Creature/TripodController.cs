using UnityEngine;
using System.Collections;

public class TripodController : MonoBehaviour {

	// Tripod States

	public const int EXPLORATION = 1;
	public const int AIMING = 2;
	public const int FIRING = 3;

	// Misc data

	NavMeshAgent nav;
	TripodHealth health;
	Animator anim;

	int playerMask;

	// Aiming and Firing

	public GameObject target;
	public float detectionRadius;
	public float maxAimingDistance;
	public float aquisitionTime;
	public float loseTargetTime;

	float timer;
	bool acquiring;
	int state;

	float firstFire = 0.1f;
	float secondFire = 0.2f;
	int shots;

	// Weapon Specs

	public GameObject bulletPrefab;
	GameObject barrel;

	public float damage;
	public float damageDecrease;
	public float speed;
	public float maxDeviation;
	public float range;
	public float autoGuidanceStart;


	void Start () {
		playerMask = LayerMask.GetMask ("Player");
		nav = GetComponent<NavMeshAgent> ();
		health = transform.Find ("hips").GetComponent<TripodHealth> ();
		barrel = transform.Find ("hips/spine/head/Barrel").gameObject;
		anim = transform.GetComponent<Animator> ();
		target = null;
		timer = 0f;
		acquiring = false;
		state = EXPLORATION;
		shots = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer += Time.deltaTime;
		stateSwitch ();
	}

	void stateSwitch(){

		switch (state) {
		case EXPLORATION:
			exploration ();
			break;
		case AIMING:
			aiming ();
			break;
		case FIRING: 
			firing ();
			break;
		}
	}

	void exploration(){
		setTarget ();
		if (target != null) {
			state = AIMING;
			timer = 0f;
			anim.SetTrigger ("Idle");
			//Il vas falloir trouver un moyen de montrer au joueur que le robot vise
			//change material to redMat
			return;
		}
		//random exploration
	}

	void aiming(){
		if (timer > 3f) {
		
			state = FIRING;
			anim.SetTrigger ("Fire");
			timer = 0f;
		}
	}

	void firing(){
		
		if (timer > firstFire && shots == 0) {
			Projectile.Create (gameObject, bulletPrefab, barrel.transform, 0f, 0, damage, damageDecrease, speed, range,true,maxDeviation,target,autoGuidanceStart); 
			shots++;
		} else if (timer > secondFire && shots == 1) {
			Projectile.Create (gameObject, bulletPrefab, barrel.transform, 0f, 0, damage, damageDecrease, speed, range,true,maxDeviation,target,autoGuidanceStart); 
			shots++;
		} else if (shots == 2) {
			shots = 0;
			state = AIMING;
			anim.SetTrigger ("Idle");
			timer = 0f;
		}
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
			float dist = Vector3.Distance (transform.position, col.gameObject.transform.position);
			if (dist < minDistance) {
				closest = col.gameObject;
				minDistance = dist;
			}
		}
		if (closest != null) {
			target = closest;
		}
	}
}

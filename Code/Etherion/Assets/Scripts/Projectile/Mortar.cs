using UnityEngine;
using System.Collections;

public class Mortar : Projectile {

	// This kind of Projectile must have a non-Kinematic Rigidbody who reacts to gravity

	protected float acceleration;
	protected float ascendingTime;

	protected float damage;
	protected float damageRadius;
	protected float damageDecrease;
	protected int side;
	protected bool descentTrigger;
	protected bool dead;
	protected bool friendFire;

	Rigidbody Rb;
	MeshRenderer mesh;
	Collider collider;



	Quaternion targetRotation;

	public GameObject[] accelerationEffects;
	public GameObject[] explosionEffects;

	float deathDelay = 2f;

	void Start () {
		Rb = GetComponent<Rigidbody> ();
		mesh = transform.Find ("missile").GetComponent<MeshRenderer> ();
		collider = GetComponent<Collider> ();
		descentTrigger = false;
		dead = false;
		targetRotation =  Quaternion.Euler (90f, 0f, 0f);
	}

	public static GameObject Create(GameObject owner, GameObject prefab, Transform barrel, float speed, float range, int side, float acceleration, float ascendingTime, float damage, float damageRadius, float damageDecrease, bool friendFire){
		GameObject projectile = Projectile.Create (owner, prefab, barrel,speed,range);
		Mortar mortar = projectile.GetComponent<Mortar> ();
		mortar.acceleration = acceleration;
		mortar.ascendingTime = ascendingTime;
		mortar.damage = damage;
		mortar.damageRadius = damageRadius;
		mortar.damageDecrease = damageDecrease;
		mortar.friendFire = friendFire;
		mortar.side = side;
		return projectile;
	}

	void FixedUpdate () {

		if (dead) {
			timer += Time.fixedDeltaTime;
			if (timer >= deathDelay) {
				Delete ();
			}
			return;
		}

		base.FixedUpdate ();

		if (timer < ascendingTime) {
			speed += acceleration * Time.fixedDeltaTime;
			Rb.AddForce (transform.forward * speed * Time.fixedDeltaTime);
		} else {
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, .3f * Time.fixedDeltaTime);
			if (!descentTrigger) {
				descentTrigger = true;
				Rb.useGravity = true;
				foreach (GameObject go in accelerationEffects) {
					go.SetActive (false);
				}
			}


		}


	}

	void OnCollisionEnter(Collision col){
		if (!dead) {
			foreach (GameObject go in explosionEffects) {
				go.SetActive (true);
			}
			Rb.isKinematic = true;
			Rb.useGravity = false;
			mesh.enabled = false;
			collider.enabled = false;
			dead = true;
			timer = 0f;
			DoDamage ();
		}
	}

	protected void DoDamage(){

		bool shot;

		Collider[] hitColliders = Physics.OverlapSphere (transform.position, damageRadius, playerMask | creatureMask);
		foreach (Collider col in hitColliders) {
			GameObject go = col.gameObject;
			float dist = Vector3.Distance (transform.position, col.gameObject.transform.position);
			float adjustedDamage = damage - (damageDecrease * dist);
			Debug.Log (go.layer);

		
			if (IsPlayerLayer (col.gameObject.layer)) {
				PlayerHealth health = go.GetComponent<PlayerHealth> ();
				PlayerState state = go.GetComponent<PlayerState> ();
				if (health != null && !health.dead && (side != state.side || friendFire)) {
					health.TakeDamage (adjustedDamage, side);
					shot = true;
				}
			} else if (IsCreatureLayer (col.gameObject.layer)) {
				CreatureHealth health = go.GetComponent<CreatureHealth> ();
				if (health != null && !health.dead) {
					health.TakeDamage (adjustedDamage, owner);
					shot = true;
				}
			}

		}
	}
		
}

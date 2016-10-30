using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	int side;
	float initialDamage;
	float damageDecrease;
	float speed;
	float range;
	GameObject owner;

	float autoGuidanceStart = .05f;

	bool autoGuidance;
	float maxDeviation;
	GameObject target;

	Vector3 initialPosition;

	float timer;


	void Start () {
		timer = 0f;
	}

	public static void Create(GameObject owner, GameObject prefab, Transform barrel, float dispertion, int side, float initialDamage, float damageDecrease, float speed, float range,
		bool autoGuidance = false,float maxDeviation = 0f, GameObject target = null,float autoguidanceStart = .05f){

		GameObject projectile = (GameObject) Instantiate (prefab, barrel.position,barrel.rotation);
		projectile.transform.Rotate (new Vector3(Random.Range (-dispertion, dispertion),Random.Range (-dispertion, dispertion),Random.Range (-dispertion, dispertion)));
		Projectile p = projectile.GetComponent<Projectile> ();
		p.side = side;
		p.initialDamage = initialDamage;
		p.damageDecrease = damageDecrease;
		p.speed = speed;
		p.range = range;
		p.owner = owner;
		p.initialPosition = projectile.transform.position;
		p.autoGuidance = autoGuidance;
		p.maxDeviation = maxDeviation;
		p.target = target;
	}

	void FixedUpdate () {
		timer += Time.deltaTime;

		if (autoGuidance && timer > autoGuidanceStart) {
			Vector3 direction = (target.transform.position - transform.position);//.normalized;

			direction = Vector3.RotateTowards (transform.forward, direction, maxDeviation * Time.fixedDeltaTime, 0.0f);

			transform.rotation = Quaternion.LookRotation (direction);

			//transform.rotation = Quaternion.Lerp (transform.rotation,Quaternion.Euler (direction), maxDeviation * Time.fixedDeltaTime);
		}

		transform.position += transform.forward * speed * Time.deltaTime;
			
		if ( Vector3.Distance (initialPosition, transform.position) > range){
			Delete ();
		}
	}
		
		

	protected void OnTriggerEnter(Collider other) {
		
		float damage = Mathf.Max (initialDamage - (damageDecrease * timer),0f);

		if (other.tag.Equals ("Player")) {
			PlayerState state = other.GetComponent<PlayerState> ();
			if (state.side != side){
				PlayerHealth health = other.GetComponent<PlayerHealth> ();
				if (health != null) {
					health.TakeDamage (damage,side);
				}
			}
		}else if (other.tag.Equals ("Creature")){
			CreatureHealth health = other.GetComponent<CreatureHealth> ();
			if (health != null) {
				health.TakeDamage (damage, owner);
			}
		}
		Delete ();
	}

	void Delete(){
		foreach (Transform child in transform){
			Destroy (child.gameObject);
		}
		Destroy (gameObject);
	}
}

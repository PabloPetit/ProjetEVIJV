using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	int side;
	float initialDamage;
	float damageDecrease;
	float speed;
	float range;
	GameObject owner;

	Vector3 initialPosition;

	float timer;


	void Start () {
		timer = 0f;
	}

	public static void Create(GameObject owner, GameObject prefab, Transform barrel, float dispertion, int side, float initialDamage, float damageDecrease, float speed, float range){
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
	}

	void Update () {
		timer += Time.deltaTime;
		transform.position += transform.forward * speed * Time.deltaTime;
		if ( Vector3.Distance (initialPosition, transform.position) > range){
			Delete ();
		}
	}
		
		

	protected void OnTriggerEnter(Collider other) {
		if (other.tag.Equals ("Player")) {
			PlayerState state = other.GetComponent<PlayerState> ();
			if (state.side != side){
				PlayerHealth health = other.GetComponent<PlayerHealth> ();
				if (health != null) {
					float damage = Mathf.Max (initialDamage - (damageDecrease * timer),0f); 
					health.TakeDamage (damage,side);
				}
			}
		}else if (other.tag.Equals ("Creature")){
			CreatureHealth health = other.GetComponent<CreatureHealth> ();
			float damage = Mathf.Max (initialDamage - (damageDecrease * timer),0f); 
			health.TakeDamage (damage, owner);
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

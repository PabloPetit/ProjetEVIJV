using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected int side;
	protected float initialDamage;
	protected float damageDecrease;
	protected float speed;
	protected float range;

	protected Vector3 initialPosition;

	protected float timer;

	void Start () {
		timer = 0f;
	}

	public static void Create(GameObject prefab, Transform barrel, float dispertion, int side, float initialDamage, float damageDecrease, float speed, float range){
		GameObject projectile = (GameObject) Instantiate (prefab, barrel.position,barrel.rotation);
		projectile.transform.Rotate (new Vector3(Random.Range (-dispertion, dispertion),Random.Range (-dispertion, dispertion),Random.Range (-dispertion, dispertion)));
		Projectile p = projectile.GetComponent<Projectile> ();
		p.side = side;
		p.initialDamage = initialDamage;
		p.damageDecrease = damageDecrease;
		p.speed = speed;
		p.range = range;
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
		Debug.Log ("HIT");

		if (other.tag.Equals ("Shootable")) {
			PlayerHealth health = other.GetComponent<PlayerHealth> ();

			if (health != null) {
				float damage = Mathf.Max (initialDamage - (damageDecrease * timer)); 
				health.TakeDamage (damage,side);
			}
		}

		Delete ();
	}

	void Delete(){
		Debug.Log ("Deleted");
		foreach (Transform child in transform){
			Destroy (child.gameObject);
		}
		Destroy (gameObject);
	}
}

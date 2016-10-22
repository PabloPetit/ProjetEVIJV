using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public int side;
	public  float initialDamage;
	public float damageDecrease;
	public float speed;
	public float range;

	protected Vector3 position;
	protected Vector3 direction;

	protected float timer;


	void Start () {
		timer = 0f;
		endStart ();
	}
		
	protected virtual void endStart(){}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;
		transform.position += transform.forward * speed * Time.deltaTime;
		endUpdate ();
		if ( Vector3.Distance (position, transform.position) > range){
			Delete ();
		}
	}

	protected virtual void endUpdate(){}
		

	void OnCollisionEnter(Collision collision) {

		GameObject other = collision.collider.gameObject;
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

		foreach (Transform child in transform){
			Destroy (child.gameObject);
		}
		Destroy (gameObject);
	}
}

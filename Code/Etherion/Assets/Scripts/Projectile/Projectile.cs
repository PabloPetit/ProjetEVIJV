using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected float speed;
	protected float range;

	protected GameObject owner;

	protected Vector3 initialPosition;
	protected float timer;

	//protected Transform barrel;

	public virtual void Start () {
		timer = 0f;
	}


	public static GameObject Create(GameObject owner, GameObject prefab ,Transform barrel, float speed, float range){
		GameObject projectile = (GameObject) Instantiate (prefab, barrel.position,barrel.rotation);
		Projectile p = projectile.GetComponent<Projectile> ();
		p.speed = speed;
		p.range = range;
		p.owner = owner;
		p.initialPosition = projectile.transform.position;
		return projectile;
	}

	protected virtual void FixedUpdate () {
		timer += Time.fixedDeltaTime;
		transform.position += transform.forward * speed * Time.deltaTime;

		if ( Vector3.Distance (initialPosition, transform.position) > range){
			Delete ();
		}
			
	}
		

	protected virtual void OnTriggerEnter(Collider other) {
		Delete ();
	}

	protected void Delete(float delay=0f){
		foreach (Transform child in transform){
			Destroy (child.gameObject,delay);
		}
		Destroy (gameObject,delay);
	}
}

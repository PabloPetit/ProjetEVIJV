using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	protected float speed;
	protected float range;

	protected GameObject owner;

	protected Vector3 initialPosition;
	protected float timer;

	protected HitMarker hitMarkerUI;
	protected bool hitMarker;

	//protected Transform barrel;

	public int playerMask;
	public int creatureMask;

	public void Awake(){
		playerMask = LayerMask.GetMask ("Player");
		creatureMask = LayerMask.GetMask ("Creature");
		hitMarkerUI = GameObject.Find ("HitMarker").GetComponent<HitMarker> ();
	}

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
		//TODO : Delay not working
		foreach (Transform child in transform){
			Destroy (child.gameObject,t:delay);
		}
		Destroy (gameObject,t:delay);
	}

	public bool IsCreatureLayer(int layer){
		return ((1 << layer) & creatureMask) != 0;
	}

	public bool IsPlayerLayer(int layer){
		return ((1 << layer) & playerMask) != 0;
	}

}

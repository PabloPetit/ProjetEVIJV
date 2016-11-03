using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

	public static float MAX_BULLET_RANGE = 6000f;

	public int playerMask;
	public int creatureMask;

	protected float speed;
	protected float range;
	protected GameObject owner;
	protected Vector3 initialPosition;
	protected float timer;




	public void Awake ()
	{
		playerMask = LayerMask.GetMask ("Player");
		creatureMask = LayerMask.GetMask ("Creature");
	}

	public virtual void Start ()
	{
		timer = 0f;
	}


	public static GameObject Create (GameObject owner, GameObject prefab, Transform barrel, float speed)
	{
		GameObject projectile = (GameObject)Instantiate (prefab, barrel.position, barrel.rotation);
		Projectile p = projectile.GetComponent<Projectile> ();
		p.speed = speed;
		p.range = MAX_BULLET_RANGE;
		p.owner = owner;
		p.initialPosition = projectile.transform.position;
		return projectile;
	}

	protected virtual void FixedUpdate ()
	{
		timer += Time.fixedDeltaTime;
		transform.position += transform.forward * speed * Time.deltaTime;
		if (Vector3.Distance (initialPosition, transform.position) > range) {
			Delete ();
		}
	}


	protected virtual void OnTriggerEnter (Collider other)
	{
		Delete ();
	}

	protected void Delete (float delay = 0f)
	{
		foreach (Transform child in transform) {
			Destroy (child.gameObject, t: delay);
		}
		Destroy (gameObject, t: delay);
	}

	public bool IsCreatureLayer (int layer)
	{
		return ((1 << layer) & creatureMask) != 0;
	}

	public bool IsPlayerLayer (int layer)
	{
		return ((1 << layer) & playerMask) != 0;
	}

}

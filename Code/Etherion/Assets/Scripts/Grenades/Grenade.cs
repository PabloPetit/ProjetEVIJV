using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour, IDamage
{

	public Player shooter { get ; set ; }

	public bool friendlyFire { get ; set ; }

	public float initialDamage { get ; set ; }

	public float minDamage { get ; set ; }

	public float damageDecrease { get ; set ; }

	public float radius;

	public float gravityEnforcement;

	protected float deathDelay;

	int ignoreMask;

	public GameObject[] toEnable;

	protected bool hasCollide;

	protected AudioSource audio;

	protected Rigidbody rb;

	public virtual void Start ()
	{
		ignoreMask = LayerMask.GetMask ("IgnoreBulletCollision");
		hasCollide = false;
		audio = GetComponent<AudioSource> ();
		rb = GetComponent<Rigidbody> ();
	}

	public void FixedUpdate ()
	{
		rb.velocity = rb.velocity + Vector3.down * Time.fixedTime * gravityEnforcement;
	}

	public static GameObject Create (GameObject prefab, Vector3 position, Vector3 velocity, Player shooter,
	                                 bool friendlyFire, float initialDamage, float minDamage, float damageDecrease)
	{

		GameObject gre = Instantiate (prefab);
		gre.transform.position = position;

		Rigidbody rb = gre.GetComponent<Rigidbody> ();
		rb.velocity = velocity;

		rb.AddTorque (Random.onUnitSphere.normalized * 10f);

		Grenade grenade = gre.GetComponent<Grenade> ();
		grenade.shooter = shooter;
		grenade.friendlyFire = friendlyFire;
		grenade.initialDamage = initialDamage;
		grenade.minDamage = minDamage;
		grenade.damageDecrease = damageDecrease;
		return gre;
	}

	protected virtual void OnCollisionEnter (Collision col)
	{
		if (((1 << col.gameObject.layer) & ignoreMask) != 0) {
			return;
		}
		hasCollide = true;
		audio.Play ();
		EnableEffects ();
		Damage.DoZoneDamage (shooter, transform, radius, this);
		rb.velocity = Vector3.zero;
		Delete (deathDelay);
	}

	protected void EnableEffects ()
	{
		foreach (GameObject go in toEnable) {
			go.SetActive (true);
		}
	}

	protected void Delete (float delay = 0f)
	{
		foreach (Transform child in transform) {
			Destroy (child.gameObject, t: delay);
		}
		Destroy (gameObject, t: delay);
	}

}

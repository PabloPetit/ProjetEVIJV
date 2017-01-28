﻿using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{

	public static float DEFAULT_RANGE = 6000f;

	public AudioSource audio;

	protected float speed;
	protected float range;

	protected Vector3 initialPosition;
	protected float timer;

	protected int ignoreCollisionLayer;

	public bool playAudio = true;

	public void Awake ()
	{
		audio = GetComponent<AudioSource> ();
	}

	public virtual void Start ()
	{
		timer = 0f;
		if (audio != null && playAudio) {
			audio.Play ();
		}
		ignoreCollisionLayer = LayerMask.GetMask ("IgnoreBulletCollision");
	}


	public static GameObject Create (GameObject prefab, Transform barrel, float speed, float dispertion)
	{
		GameObject projectile = (GameObject)Instantiate (prefab, barrel.position, barrel.rotation);
		projectile.transform.Rotate (new Vector3 (Random.Range (-dispertion, dispertion), Random.Range (-dispertion, dispertion), Random.Range (-dispertion, dispertion)));
		Projectile p = projectile.GetComponent<Projectile> ();
		p.speed = speed;
		p.range = DEFAULT_RANGE;
		p.initialPosition = projectile.transform.position;
		return projectile;
	}

	protected virtual void FixedUpdate ()
	{
		timer += Time.fixedDeltaTime;
		transform.position += transform.forward * speed * Time.fixedDeltaTime;
		if (Vector3.Distance (initialPosition, transform.position) > range) {
			Delete ();
		}
	}


	protected virtual void OnTriggerEnter (Collider other)
	{
		if (((ignoreCollisionLayer) & (1 << other.gameObject.layer)) > 0) {
			return;
		}

		Delete ();
	}

	protected void Delete (float delay = 0f)
	{
		foreach (Transform child in transform) {
			Destroy (child.gameObject, t: delay);
		}
		Destroy (gameObject, t: delay);
	}



}

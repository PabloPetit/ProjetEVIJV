using UnityEngine;
using System.Collections;

public class BalisticShot : Projectile
{

	protected float gravityDeviation = .3f;
	protected float gravityForce = 9.81f;
	protected Quaternion targetRotation;

	protected float acceleration;
	protected float ascendingTime;
	protected float maxDescendingSpeed;
	protected float deathDelay;

	protected bool descent;
	protected bool dead;
	protected MeshRenderer renderer;
	protected Collider collider;

	public void Start ()
	{
		dead = false;
		descent = false;
		targetRotation = Quaternion.Euler (90f, 0f, 0f);
		this.renderer = GetComponentInChildren<MeshRenderer> ();
		this.collider = GetComponent<Collider> ();
	}

	public static GameObject Create (GameObject prefab, Transform barrel, float speed, float dispertion, float acceleration, float ascendingTime, float maxDescendingSpeed, float deathDelay)
	{
		GameObject pro = Projectile.Create (prefab, barrel, speed, dispertion);
		BalisticShot balistic = pro.GetComponent<BalisticShot> ();

		balistic.acceleration = acceleration;
		balistic.maxDescendingSpeed = maxDescendingSpeed;
		balistic.deathDelay = deathDelay;
		balistic.ascendingTime = ascendingTime;

		return pro;
	}

	void FixedUpdate ()
	{
		if (dead)
			return;

		base.FixedUpdate ();


		if (timer < ascendingTime) {
			speed += acceleration * Time.fixedDeltaTime;
		} else {
			if (!descent) {
				StartDescent ();
			}

			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, gravityDeviation * Time.fixedDeltaTime);
		}
	}

	protected virtual void StartDescent ()
	{
		descent = true;
	}

	protected override void OnTriggerEnter (Collider other)
	{
		Delete (deathDelay);
		dead = true;
		this.renderer.enabled = false;
		this.collider.enabled = false;
	}

}

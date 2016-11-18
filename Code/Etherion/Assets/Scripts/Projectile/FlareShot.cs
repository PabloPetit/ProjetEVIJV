using UnityEngine;
using System.Collections;

public class FlareShot : BalisticShot
{
	protected float intensity;
	protected float lightRange;
	protected Light flareLight;
	protected Flare flare;

	public float maxDownSpeed = 2f;

	Rigidbody rb;

	public void Start ()
	{
		base.Start ();
		rb = GetComponent<Rigidbody> ();
		flareLight = GetComponent<Light> ();
		flare = flareLight.flare;
		flareLight.flare = null;

	}

	public static GameObject Create (GameObject prefab, Transform barrel, float speed, float dispertion, float acceleration, float ascendingTime, float maxDescendingSpeed, float deathDelay,
	                                 float intensity, float lightRange)
	{

		GameObject pro = BalisticShot.Create (prefab, barrel, speed, dispertion, acceleration, ascendingTime, maxDescendingSpeed, deathDelay);

		FlareShot flareShot = pro.GetComponent<FlareShot> ();
		flareShot.intensity = intensity;
		flareShot.lightRange = lightRange;

		return pro;
	}

	protected override void FixedUpdate ()
	{
		Debug.Log (rb.velocity.y);

		if (!descent) {
			base.FixedUpdate ();
		} else {
			

			rb.velocity = new Vector3 (rb.velocity.x, Mathf.Max (rb.velocity.y, -maxDownSpeed), rb.velocity.z);
		}
	}


	protected override void StartDescent ()
	{
		base.StartDescent ();
		flareLight.flare = flare;
		flareLight.intensity = intensity;
		flareLight.range = lightRange;
		gameObject.AddComponent<Rigidbody> ();

		rb.useGravity = true;
		rb.isKinematic = false;
	}

}

using UnityEngine;
using System.Collections;

public class FlareShot : BalisticShot
{
	protected float intensity;
	protected float lightRange;
	protected Light flareLight;
	protected Flare flare;


	public void Start ()
	{
		base.Start ();
		flareLight.flare = null;
		flareLight = GetComponent<Light> ();
		flare = flareLight.flare;
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


	protected override void StartDescent ()
	{
		flareLight.flare = flare;
		flareLight.intensity = intensity;
		flareLight.range = lightRange;
	}

}

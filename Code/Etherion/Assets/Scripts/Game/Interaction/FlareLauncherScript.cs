using UnityEngine;
using System.Collections;

public class FlareLauncherScript : Interaction
{



	public float interval;

	public float speed;
	public float range;
	public float acceleration;
	public float ascendingTime;
	public float descendingSpeed;
	public float intensity;
	public float lightRange;
	public float deathDelay;

	public GameObject barrel;
	public GameObject prefab;
	AudioSource audio;
	float timer;

	void Start ()
	{
		timer = interval;
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
	}

	public override void Action ()
	{
		if (timer > interval) {
			FlareShot.Create (prefab, barrel.transform, speed, 0f, acceleration, ascendingTime, descendingSpeed, 1f, intensity, lightRange);
			timer = 0f;
			audio.Play ();
		}
	}
}

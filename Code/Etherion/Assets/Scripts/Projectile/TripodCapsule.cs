using UnityEngine;
using System.Collections;

public class TripodCapsule : Projectile
{

	public GameObject tripodPrefab;

	float delay = 1.9f;

	public GameObject effects;

	bool done;

	public static GameObject Create (GameObject prefab, Transform barrel, float speed, float dispertion,
	                                 GameObject tripodPrefab)
	{
		GameObject pro = Projectile.Create (prefab, barrel, speed, dispertion);
		TripodCapsule capsule = pro.GetComponent<TripodCapsule> ();
		capsule.tripodPrefab = tripodPrefab;
		return pro;
	}

	public override void Start ()
	{
		
	}


	protected override void OnTriggerEnter (Collider other)
	{
		if (!done) {
			GameObject go = (GameObject)Instantiate (tripodPrefab);
			go.transform.position = transform.position;
			GameObject go2 = (GameObject)Instantiate (effects);
			go2.transform.position = transform.position;
			audio.Play ();
			Delete (delay);
			Destroy (go2, delay);
			done = true;
		}
	}

}

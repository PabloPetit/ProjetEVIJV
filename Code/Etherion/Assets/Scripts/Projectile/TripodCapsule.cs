using UnityEngine;
using System.Collections;

public class TripodCapsule : Projectile {

	public GameObject tripodPrefab;


	public static GameObject Create (GameObject prefab, Transform barrel, float speed, float dispertion,
		GameObject tripodPrefab)
	{
		GameObject pro = Projectile.Create (prefab, barrel, speed, dispertion);
		TripodCapsule capsule = pro.GetComponent<TripodCapsule> ();
		capsule.tripodPrefab = tripodPrefab;
		return pro;
	}


	protected override void OnTriggerEnter (Collider other)
	{
		GameObject go = (GameObject)Instantiate (tripodPrefab);
		go.transform.position = transform.position;
		Delete ();
	}

}

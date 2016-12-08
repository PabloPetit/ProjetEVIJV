using UnityEngine;
using System.Collections;

public class TripodSpawner : MonoBehaviour {

	public GameObject tripodPrefab;
	public GameObject capsule;
	public float speed = 1000f;

	public GameObject[] targets;


	void Update(){

		if (Input.GetKeyDown (KeyCode.H)){

			GameObject target = targets[Random.Range (0, targets.Length)];
			transform.LookAt (target.transform.position);
			TripodCapsule.Create (capsule,transform,speed,0f,tripodPrefab);
		}

	}

}

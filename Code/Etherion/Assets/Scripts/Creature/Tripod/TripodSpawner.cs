using UnityEngine;
using System.Collections;

public class TripodSpawner : MonoBehaviour
{

	public static string CALL_TRIPOD = "callTripod";

	public GameObject tripodPrefab;
	public GameObject capsule;
	public float speed = 1000f;

	public GameObject[] targets;

	EventName callTripod;


	void Start ()
	{
		SkyManager skyManager = FindObjectOfType<SkyManager> ();
		callTripod = new EventName (CALL_TRIPOD);
		EventManager.StartListening (callTripod, SendTripod);
		skyManager.AddNightCallBack (new object[]{ callTripod, new object[]{ } });
	}


	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.H)) {
			//Only for Debug
			SendTripod (null);
		}

	}

	public void SendTripod (object[] param)
	{
		GameObject target = targets [Random.Range (0, targets.Length)];
		transform.LookAt (target.transform.position);
		TripodCapsule.Create (capsule, transform, speed, 0f, tripodPrefab);
	}

}

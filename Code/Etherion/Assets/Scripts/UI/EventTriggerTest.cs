using UnityEngine;
using System.Collections;

public class EventTriggerTest : MonoBehaviour
{


	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Key is Down");
			EventManager.TriggerEvent ("test");
		}
	}
}

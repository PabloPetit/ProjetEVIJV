using UnityEngine;
using System.Collections;

public class EventTriggerTest : MonoBehaviour
{


	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Key is Down");
			EventManager.TriggerAction ("test", 3.14f);
		}
	}
}

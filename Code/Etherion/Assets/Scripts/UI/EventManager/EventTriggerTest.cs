using UnityEngine;
using System.Collections;

public class EventTriggerTest : MonoBehaviour
{
	private EventName trigger;

	void Awake(){
		trigger = new EventName ("test");
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Key is Down");
			EventManager.TriggerAction (trigger, 3.14f);
		}
	}
}

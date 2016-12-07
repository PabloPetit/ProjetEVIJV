using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkyManager : MonoBehaviour
{

	public static float DAY_LENGTH = 24f;
	public static float MORNING = 7f;
	public static float NOON = 12f;
	public static float NIGHT = 17f;

	public float speed = 0.8f;
	public Vector3 axis = Vector3.forward;

	public float time;
	public int hours;
	public int minutes;
	public int seconds;

	public List<EventName> nightCallBack;
	public List<EventName> dayCallBack;

	public bool day;

	public void Awake ()
	{
		nightCallBack = new List<EventName> ();
		dayCallBack = new List<EventName> ();
		day = (time > MORNING && time < NIGHT);
	}

	void FixedUpdate ()
	{
		gameObject.transform.RotateAround (Vector3.zero, axis, speed * Time.fixedDeltaTime);
		SetHour ();

		if (day && !(time > MORNING && time < NIGHT)) {
			day = false;
			ProcessNightTransition ();
		} else if (!day && (time > MORNING && time < NIGHT)) {
			day = true;
			ProcessDayTransition ();
		}
	}

	void SetHour ()
	{
		time = (12f + (transform.rotation.eulerAngles.z) / (360f / 24f)) % 24f;
		hours = (int)time;
		minutes = (int)((time % 1f) * 60f);
		seconds = (int)((time % 1f) * .6f);
	}

	public void AddNightCallBack (EventName eventName)
	{
		nightCallBack.Add (eventName);
	}

	public void AddDayCallBack (EventName eventName)
	{
		dayCallBack.Add (eventName);
	}

	void ProcessNightTransition ()
	{
		foreach (EventName ev in nightCallBack) {
			EventManager.TriggerAction (ev, null);
		}
	}

	void ProcessDayTransition ()
	{
		foreach (EventName ev in dayCallBack) {
			EventManager.TriggerAction (ev, null);
		}
	}
		
}

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

	public List<object[]> nightCallBack;
	public List<object[]> dayCallBack;

	public bool day;

	void Awake ()
	{
		nightCallBack = new List<object[]> ();
		dayCallBack = new List<object[]> ();
		SetHour ();
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

	public void AddNightCallBack (object[] param)
	{
		nightCallBack.Add (param);
	}

	public void AddDayCallBack (object[] param)
	{
		dayCallBack.Add (param);
	}

	void ProcessNightTransition ()
	{
		Debug.Log ("NightTransition");
		foreach (object[] ev in nightCallBack) {
			Debug.Log (((EventName)ev[0]).name);
			EventManager.TriggerAction ((EventName)ev[0], (object[])ev[1]);
		}
	}

	void ProcessDayTransition ()
	{
		Debug.Log ("DayTransition");
		foreach (object[] ev in nightCallBack) {
			Debug.Log (((EventName)ev[0]).name);
			EventManager.TriggerAction ((EventName)ev[0],  (object[])ev[1]);
		}
	}
		
}

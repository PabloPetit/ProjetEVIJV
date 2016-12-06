using UnityEngine;
using System.Collections;

public class SkyManager : MonoBehaviour {

	public static float DAY_LENGTH = 24f;
	public static float MORNING = 8f;
	public static float NOON = 12f;
	public static float NIGHT = 20f;

	public float speed = 0.8f;
	public Vector3 axis = Vector3.forward;

	public float time;
	public int hours;
	public int minutes;
	public int seconds;

	public ArrayList nightCallBack;
	public ArrayList dayCallBack;


	void Start(){
		
	}

	void FixedUpdate(){
		gameObject.transform.RotateAround (Vector3.zero, axis, speed * Time.fixedDeltaTime);
		SetHour ();
		Debug.Log (time+" - Heure  "+hours+" : "+minutes+" : "+seconds);
	}

    void SetHour(){
		time = (12f + (transform.rotation.eulerAngles.z) / (360f / 24f)) % 24f;
		hours = (int)time;
		minutes = (int)((time % 1f) * 60f);
		seconds =  (int)((time % 0.01f) * 60f);
	}
		
}

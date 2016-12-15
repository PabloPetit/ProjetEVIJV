using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class HitMarker : MonoBehaviour
{

	public static string HITMARKER_CHANNEL = "hitMarker";

	public float fadeSpeed = 1f;
	RawImage hitMarker;
	private EventName hitEvent;
	private AudioSource audio;

	void Awake ()
	{
		hitMarker = GetComponent<RawImage> ();
		audio = GetComponent<AudioSource> ();
		hitEvent = new EventName (HITMARKER_CHANNEL);
	}

	void Update ()
	{
		Color c = hitMarker.color;
		c.a -= fadeSpeed * Time.deltaTime;
		c.a = Mathf.Max (c.a, 0f);
		hitMarker.color = c;
	}

	void Hit (object[] param)
	{
		Color c = hitMarker.color;
		c.a = 1f;
		hitMarker.color = c;
		audio.Play ();
	}

	void OnEnable ()
	{
		EventManager.StartListening (hitEvent, new System.Action<object[]> (Hit));
	}

	void OnDisable ()
	{
		EventManager.StopListening (hitEvent);
	}

}

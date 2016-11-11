using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BloodCanvas : MonoBehaviour
{

	public static string BLOOD_CANVAS_CHANNEL = "bloodCanvas";

	RawImage hitMarker;
	private EventName hitEvent;


	void Awake ()
	{
		hitMarker = GetComponent<RawImage> ();
		hitEvent = new EventName (BLOOD_CANVAS_CHANNEL);
	}


	void Set (object[] param = null)
	{
		float percent = (float)param [0];

		Color c = hitMarker.color;
		c.a = 1f - percent;
		hitMarker.color = c;
	}

	void OnEnable ()
	{
		EventManager.StartListening (hitEvent, new System.Action<object[]> (Set));
	}

	void OnDisable ()
	{
		EventManager.StopListening (hitEvent);
	}
}

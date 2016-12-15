using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverloadBar : MonoBehaviour
{

	public static string OVERLOAD_BAR_CHANNEL = "overloadBar";
	private EventName overloadEvent;
	private Image image;

	void Start ()
	{
		image = GetComponent<Image> ();
	}

	void OnEnable ()
	{
		overloadEvent = new EventName (OVERLOAD_BAR_CHANNEL);
		EventManager.StartListening (overloadEvent, new System.Action<object[]> (Maj));
	}

	void OnDisable ()
	{
		EventManager.StopListening (overloadEvent);
	}

	void Maj (object[] param)
	{
		float val = (float)param [0];
		bool overloaded = (bool)param [1];
		
		image.fillAmount = val;

		if (overloaded) {
			image.color = Color.red;
		} else {
			image.color = Color.white;
		}
	}
}

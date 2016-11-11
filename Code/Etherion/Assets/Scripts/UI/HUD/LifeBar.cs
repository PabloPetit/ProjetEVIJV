using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{

	public static string LIFE_BAR_CHANNEL = "lifeBar";

	Slider slider;

	private EventName lifeBarEvent;

	void Start ()
	{
		slider = GetComponent<Slider> ();

	}


	void Set (object[] param = null)
	{
		float percent = (float)param [0];

		slider.value = slider.maxValue * percent;
	}

	void OnEnable ()
	{
		lifeBarEvent = new EventName (LIFE_BAR_CHANNEL);
		EventManager.StartListening (lifeBarEvent, new System.Action<object[]> (Set));
	}

	void OnDisable ()
	{
		EventManager.StopListening (lifeBarEvent);
	}
}

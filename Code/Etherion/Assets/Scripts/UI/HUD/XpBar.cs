using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{

	public static string XP_BAR_CHANNEL = "XPBar";

	Slider slider;
	Text text;

	private EventName XPBarEvent;

	float timer = 0f;

	// Use this for initialization
	void Start ()
	{
		slider = GetComponentInChildren<Slider> ();
		text = GetComponentInChildren<Text> ();
	}


	void Update ()
	{
		timer += Time.deltaTime;
	}

	void Set (object[] param)
	{
		
		int level = (int)param [0];
		float totalXp = (float)param [1];
		float pastStep = (float)param [2];
		float nextStep = (float)param [3];

		text.text = "Level " + level;

		float val = (totalXp - pastStep) / (nextStep - pastStep);
		val = Mathf.Max (val, 0f);
		Debug.Log (val + " Level : " + level + " total : " + totalXp + " past : " + pastStep + " next : " + nextStep);
		slider.value = val;
	}

	void OnEnable ()
	{
		XPBarEvent = new EventName (XP_BAR_CHANNEL);
		EventManager.StartListening (XPBarEvent, new System.Action<object[]> (Set));
	}

	void OnDisable ()
	{
		EventManager.StopListening (XPBarEvent);
	}


}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{

	public static string LIFE_BAR_CHANNEL = "lifeBar";

	Slider slider;

	private EventName lifeBarEvent;

	private Image image;

	private GameObject fillArea;

	void Start ()
	{
		slider = GetComponent<Slider> ();
		image = GetComponentInChildren<Image> ();
		fillArea = transform.Find ("Fill Area").gameObject;
	}


	void Set (object[] param = null)
	{
		float percent = (float)param [0];
		slider.value = slider.maxValue * percent;
		if (percent < .25f) {
			image.color = Color.red;
		} else {
			image.color = Color.green;
		}
		if (percent <= 0f) {
			fillArea.SetActive (false);
		} else {
			fillArea.SetActive (true);
		}
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

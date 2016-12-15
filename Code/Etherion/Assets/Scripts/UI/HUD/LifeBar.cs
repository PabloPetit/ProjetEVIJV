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

	Color initialColor;
	Text text;

	// ADD text

	void Start ()
	{
		slider = GetComponent<Slider> ();
		text = GetComponentInChildren<Text> ();
		image = GetComponentInChildren<Image> ();
		fillArea = transform.Find ("Fill Area").gameObject;
		initialColor = image.color;
	}


	void Set (object[] param = null)
	{
		float life = (float)param [0];
		float maxLife = (float)param [1];
		float percent = life / maxLife;
		text.text = "" + (int)life + "/" + (int)maxLife;
		slider.value = slider.maxValue * percent;
		if (percent < .25f) {
			image.color = Color.red;
		} else {
			image.color = initialColor;
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

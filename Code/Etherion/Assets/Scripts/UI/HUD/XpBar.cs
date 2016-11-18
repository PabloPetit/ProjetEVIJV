using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class XpBar : MonoBehaviour
{

	public static string XP_BAR_CHANNEL = "XPBar";

	Slider slider;

	private EventName XPBarEvent;

	private Image image;

	private GameObject fillArea;

	Text text;


	// Use this for initialization
	void Start ()
	{
		slider = GetComponent<Slider> ();
		text = GetComponentInChildren<Text> ();
		image = GetComponentInChildren<Image> ();
		fillArea = transform.Find ("Fill Area").gameObject;
	
	}

	void Set (object[] param = null)
	{
		

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
	
	// Update is called once per frame

}

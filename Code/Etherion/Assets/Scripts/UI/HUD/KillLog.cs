using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class KillLog : MonoBehaviour
{

	public static string KILL_LOG_CHANNEL = "killLog";

	private EventName killLogEvent;

	Text text;
	Image image;

	private float timeTillFade = 4.0f;

	private int SizeQueue = 6;

	private LimitedQueue<string> queue;

	Color panelColor;
	Color textColor;

	// Use this for initialization
	void Start ()
	{
		text = GetComponentInChildren<Text> ();
		image = GetComponent<Image> ();
		panelColor = image.color;
		textColor = text.color;
		queue = new LimitedQueue<string> (SizeQueue);
		text.text = "";
	
	}

	void Update ()
	{
		Color c = text.color;
		c.a -= (textColor.a / timeTillFade) * Time.deltaTime;
		c.a = Mathf.Max (c.a, 0f);
		text.color = c;

		Color c2 = image.color;
		c2.a -= (panelColor.a / timeTillFade) * Time.deltaTime;
		c2.a = Mathf.Max (c2.a, 0f);
		image.color = c2;

	}


	void OnEnable ()
	{
		killLogEvent = new EventName (KILL_LOG_CHANNEL);
		EventManager.StartListening (killLogEvent, new System.Action<object[]> (Maj));
	}

	void Maj (object[] param)
	{
		Debug.Log ("Received");

		string txt = (string)param [0];
		queue.Enqueue (txt);

		string tmp = "";

		foreach (string str in queue) {
			tmp += str + "\n";
		}

		text.text = tmp;

		text.color = textColor;
		image.color = panelColor;

	}

	void OnDisable ()
	{
		EventManager.StopListening (killLogEvent);
	}
}

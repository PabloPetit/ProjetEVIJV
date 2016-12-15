using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLog : MonoBehaviour
{

	public static string PLAYER_LOG_CHANNEL = "playerLog";

	private EventName playLogEvent;

	Text text;
	Image image;

	private float timeTillFade = 3.0f;

	float timer;

	private int SizeQueue = 9;

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
		if (queue.Count > 0) {
			timer += Time.deltaTime;
		}


		Color c = text.color;
		c.a -= (textColor.a / timeTillFade) * Time.deltaTime;
		c.a = Mathf.Max (c.a, 0f);
		text.color = c;

		Color c2 = image.color;
		c2.a -= (panelColor.a / timeTillFade) * Time.deltaTime;
		c2.a = Mathf.Max (c2.a, 0f);
		image.color = c2;

		if (timer > timeTillFade * 2f) {
			queue.Dequeue ();
			timer = 0f;
		}

		Print ();

	}


	void OnEnable ()
	{
		playLogEvent = new EventName (PLAYER_LOG_CHANNEL);
		EventManager.StartListening (playLogEvent, new System.Action<object[]> (Maj));
	}

	void Maj (object[] param)
	{

		string txt = (string)param [0];
		queue.Enqueue (txt);
		text.color = textColor;
		image.color = panelColor;

	}

	void Print ()
	{
		string tmp = "";
		foreach (string str in queue) {
			tmp += str + "\n";
		}
		text.text = tmp;
	}

	void OnDisable ()
	{
		EventManager.StopListening (playLogEvent);
	}
}

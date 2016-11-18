using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class KillLog : MonoBehaviour
{


	public static string KILL_LOG_CHANNEL = "killLog";

	private EventName killLogEvent;

	Text text;

	private float timeTillFade = 4.0f;

	private int SizeQueue = 6;

	private LimitedQueue<string> queue;


	// Use this for initialization
	void Start ()
	{
		text = GetComponentInChildren<Text> ();
		queue = new LimitedQueue<string> (SizeQueue);

	}


	void OnEnable ()
	{
		killLogEvent = new EventName (KILL_LOG_CHANNEL);
		EventManager.StartListening (killLogEvent, new System.Action<object[]> (Set));
	}

	void OnDisable ()
	{
		EventManager.StopListening (KillLog);
	}
	// Update is called once per frame
	void Update ()
	{
	
	}
}

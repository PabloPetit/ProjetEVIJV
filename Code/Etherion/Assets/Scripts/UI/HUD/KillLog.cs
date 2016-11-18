using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class KillLog : MonoBehaviour {


	public static string KILL_LOG_CHANNEL = "killLog";

	private EventName killLogEvent;

	Text text;

	private float timeTillFade = 4.0f ;

	private int SizeQueue = 6;

	private LimitedQueue<string> Queue = new LimitedQueue<string>(SizeQueue);


	// Use this for initialization
	void Start () {
		text = GetComponentInChildren<Text> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static string Score_CHANNEL = "score";

	private EventName scoreEvent;

	Text text;

	int MaxScore = GameManager.targetScore;

	// Use this for initialization
	void Start ()
	{
		text = GetComponentInChildren<Text> ();
		text.text = "0-0 ("+;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

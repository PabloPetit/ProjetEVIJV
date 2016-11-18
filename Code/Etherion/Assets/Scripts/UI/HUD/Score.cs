using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour {

	public static string SCORE_CHANNEL = "score";

	private EventName scoreEvent;

	Text text;
	int score1;
	int score2;

	int MaxScore = GameManager.targetScore;


	void Start ()
	{
		text = GetComponentInChildren<Text> ();
		text.text = score1+ " - "+score2+" ["+MaxScore+"]";
	}


	void Set (object[] param = null)
	{
		int score1= param [0];
		int score2= param [1];
		text.text = score1+ " - "+score2+" ["+MaxScore+"]";	
	}
		
	void OnEnable ()
	{
		scoreEvent = new EventName (SCORE_CHANNEL);
		EventManager.StartListening (scoreEvent, new System.Action<object[]> (Set));
	}

	void OnDisable ()
	{
		EventManager.StopListening (scoreEvent);
	}
}

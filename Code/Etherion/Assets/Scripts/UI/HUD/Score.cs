using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Score : MonoBehaviour
{

	public static string SCORE_CHANNEL = "score";

	private EventName scoreEvent;

	Text text;
	int score1;
	int score2;

	int maxScore;


	void Start ()
	{
		maxScore = GameManager.targetScore;
		text = GetComponentInChildren<Text> ();
		text.text = score1 + " - " + score2 + " [" + maxScore + "]";
	}


	void Set (object[] param)
	{
		int[] scores = (int[])param [0];
		int score1 = scores [0];
		int score2 = scores [1];
		text.text = score1 + " - " + score2 + " [ " + maxScore + " ]";	
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

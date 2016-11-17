using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameState : MonoBehaviour
{


	public static string GAME_STATE_CHANNEL = "gameState";
	private EventName gameStateEvent;
	private Text text;

	void Start ()
	{
		text = GetComponent<Text> ();
	}

	void OnEnable ()
	{
		gameStateEvent = new EventName (GAME_STATE_CHANNEL);
		EventManager.StartListening (gameStateEvent, new System.Action<object[]> (Maj));
	}

	void OnDisable ()
	{
		EventManager.StopListening (gameStateEvent);
	}

	void Maj (object[] param)
	{
		// TODO handle teamNumber > 2

		int[] scores = (int[])param [0];
		int targetScore = (int)param [1];
		float remainingTime = (float)param [2];

		text.text = scores [0] + " - " + scores [1] + " [ " + targetScore + " ] - " + (int)(remainingTime / 60) + " : " + (int)(remainingTime % 60);
	}
}

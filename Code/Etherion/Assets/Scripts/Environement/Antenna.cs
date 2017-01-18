using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Antenna : MonoBehaviour
{

	public static int STATE_NEUTRAL = 1;
	public static int STATE_CAPTURED = 2;

	public static int MAX_LEVEL = 10;

	public static float CAPTURE_POINTS_TARGET = 100f;
	public static float POINTS_PER_PLAYER = 2.5f;

	public static float SEND_DELAY = 25f;
	public static float STD_XP = 500f;
	public static float CAPTURE_XP = 1000f;

	EventName playerLogEvent;

	public string name = "ANTENNA";
	public int level;

	float timer;
	int sendXPCount = 0;

	int playerMask;

	public SphereCollider sphCol;

	public Dictionary<Team, int> playersInside;

	public Team owners;

	public float capturePoints = 0f;
	public int state;

	void Start ()
	{
		sphCol = GetComponent<SphereCollider> ();
		playerLogEvent = new EventName (PlayerLog.PLAYER_LOG_CHANNEL);
		playerMask = LayerMask.GetMask ("Player");
		level = 1;
		state = STATE_NEUTRAL;
	}

	void SetDicts ()
	{
		if (playersInside != null) {
			return;
		}
			
		playersInside = new Dictionary<Team, int> ();
		GameManager manager = (GameManager)FindObjectOfType (typeof(GameManager));

		foreach (Team t in manager.teams) {
			playersInside.Add (t, 0);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		SetDicts (); // Called here because the GameManager do its stuff in Start()

		timer += Time.deltaTime;

		ManageCapture ();
		ManageExperience ();
	}

	public void OnTriggerEnter (Collider other)
	{
		Player p = other.gameObject.GetComponent<Player> ();
		if (p == null)
			return;
		//Debug.Log (p.name + " enterred the capture zone");
		playersInside [p.team]++;
		
	}

	public void OnTriggerExit (Collider other) // Will that work when players die inside of the collider ?
	{
		Player p = other.gameObject.GetComponent<Player> ();
		if (p == null)
			return;
		//Debug.Log (p.name + " left the capture zone");
		playersInside [p.team]--;
	}

	void ManageCapture ()
	{
		//Manage
		if (state == STATE_NEUTRAL) {
			int count = 0;
			Team tmp = null;

			foreach (Team t in playersInside.Keys) {
				if (playersInside [t] > 0) {
					count++;
					tmp = t;
				}
			}

			if (count == 1) {
				capturePoints += Time.deltaTime * POINTS_PER_PLAYER * (playersInside [tmp]);
				if (capturePoints >= CAPTURE_POINTS_TARGET) {
					state = STATE_CAPTURED;
					owners = tmp;
					timer = 0f;
					SendExperience ("Capture", CAPTURE_XP);
				}
			} else {
				capturePoints = 0f;
			}

		} else if (state == STATE_CAPTURED) {
			
			foreach (Team t in playersInside.Keys) {
				if (t == owners) {
					capturePoints += Time.deltaTime * POINTS_PER_PLAYER * playersInside [t];
				} else {
					capturePoints -= Time.deltaTime * POINTS_PER_PLAYER * playersInside [t];
				}		
			}
	
			if (capturePoints <= 0f) {
				state = STATE_NEUTRAL;
				owners = null;
				sendXPCount = 0;
			}

		}
		capturePoints = Mathf.Max (0f, Mathf.Min (CAPTURE_POINTS_TARGET, capturePoints));
	}

	void ManageExperience ()
	{
		if (state != STATE_CAPTURED) {
			return;
		}

		if (timer > SEND_DELAY) {
			timer = 0f;
			sendXPCount += 1;
			if (sendXPCount > NextLevel ()) {
				level++;
			}
			SendExperience ("", XpValue ());
		}

	}

	public int NextLevel ()
	{
		return 5 + level * 2;
	}

	public float XpValue ()
	{
		return STD_XP + STD_XP / 2 * level / 3;
	}

	void SendExperience (string mess, float xp)
	{

		string title = name + " [ " + level + " ] " + mess + " - [ +" + (int)(xp) + " XP]";
		Debug.Log ("Sending xp to : side : " + owners.side + " count : " + owners.players.Count);
		foreach (Player p in owners.players) {
			EventName xpEvent = new EventName (Player.XP_CHANNEL, p.id);
			EventManager.TriggerAction (xpEvent, new object[]{ xp });
			if (p.isHuman) {
				EventManager.TriggerAction (playerLogEvent, new object[]{ title });
			}
		}
	}


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Antenna : MonoBehaviour
{

	public static int MAX_LEVEL = 10;

	public static float CAPTURE_POINTS_TARGET = 100f;
	public static float POINTS_PER_PLAYER = 8f;

	public static float SEND_DELAY = 10f;
	public static float STD_XP = 500f;

	public string name = "ANTENNA";
	public int level;

	float timer;
	int count = 0;

	int playerMask;

	SphereCollider sphCol;

	Dictionary<Team, float> capturePoints;
	Dictionary<Team, int> playersInside;

	Team owners;

	void Start ()
	{
		sphCol = GetComponent<SphereCollider> ();
		playerMask = LayerMask.GetMask ("Player");
		level = 1;

	}

	void SetDicts ()
	{
		if (capturePoints != null) {
			return;
		}

		capturePoints = new Dictionary<Team, float> ();
		playersInside = new Dictionary<Team, int> ();
		GameManager manager = (GameManager)FindObjectOfType (typeof(GameManager));

		foreach (Team t in manager.teams) {
			capturePoints.Add (t, 0f);
			playersInside.Add (t, 0);
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		SetDicts (); // Called here because the GameManager do its stuff in Start()

		timer += Time.deltaTime;


	
	}

	void ManageCapture ()
	{
		//Clear
		foreach (Team t in playersInside) {
			playersInside [t] = 0;
		}

		Collider[] hitColliders = Physics.OverlapSphere (transform.position, sphCol.radius, playerMask);

		foreach (Collider col in hitColliders) {
			Player p = col.gameObject.GetComponent<Player> ();
			if (p == null)
				continue;

			playersInside [p.team]++;
		}

		// Get max

		// if already capture capture and max != owners : decrease capture points
		// if points == 0 owners = null

		// else if no owners and max is unique : icrease capture points



	}

	void ManageExperience ()
	{
		
	}

	void SendExperience ()
	{

		float xp = STD_XP * level * 1.742f;

		foreach (Player p in owners.players) {
			EventName xpEvent = new EventName (Player.XP_CHANNEL, p.id);
			EventManager.TriggerAction (xpEvent, new object[]{ xp });
			if (p.isHuman) {
				
			}
		}
	}


}

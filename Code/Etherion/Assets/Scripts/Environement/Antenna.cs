using UnityEngine;
using System.Collections;

public class Antenna : MonoBehaviour
{

	public int MAX_LEVEL = 10;

	public float CAPTURE_TIME = 14f;

	public float SEND_DELAY = 10f;
	public float STD_XP = 500f;

	public int level;

	float timer;
	int count = 0;

	Team owners;

	void Start ()
	{
		level = 1;


	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
	
	}

	void RetrieveExperience ()
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

using UnityEngine;
using System.Collections;

public class Experience : MonoBehaviour
{

	public static int MAX_LEVEL = 10;

	EventName xpChannel;
	EventName xpPanel;

	public float totalXp;
	public int level;

	public Player player;

	void Start ()
	{
		player = GetComponent<Player> ();
	}


	public void ReceiveXp (object[] param)
	{

		//TODO : might need to check if dead
		float xp = (float)param [0];
		totalXp += xp;

		if (totalXp > NextLevelStep (level)) {
			level += Mathf.Min (level + 1, MAX_LEVEL);
		}
		//TODO : EventManager.Trigger(xpPanel, new Object[]{xp,level});
	}

	public float NextLevelStep (int level)
	{
		return 100 + Mathf.Pow (1 + level, 3);
	}

	public float RetrievedXp ()
	{
		return level + totalXp / 100;
	}

	public object[] GetXpInfo ()
	{
		return null;
	}

	public void OpenChannel (int id)
	{
		if (xpChannel == null) {
			xpChannel = new EventName (Player.XP_CHANNEL, id);
		}

		EventManager.StartListening (xpChannel, ReceiveXp);
	}

	public void CloseChannel ()
	{
		EventManager.StopListening (xpChannel);
	}
}

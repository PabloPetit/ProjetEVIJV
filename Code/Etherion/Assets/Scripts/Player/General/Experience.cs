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
	EventName xpEvent;

	bool init = false;

	void Start ()
	{
		player = GetComponent<Player> ();
		xpEvent = new EventName (XpBar.XP_BAR_CHANNEL, player.id);
		level = 1;
		totalXp = 0;

	}

	void Update ()
	{
		if (player.isHuman && !init) {
			EventManager.TriggerAction (xpEvent, GetXpInfo ());
			init = true;
		}
	}

	public void ReceiveXp (object[] param)
	{

		if (player.health.dead) {
			return;
		}
		float xp = (float)param [0];
		//Debug.Log (xp);


		totalXp += xp;

		if (totalXp > NextLevelStep (level)) {
			level += Mathf.Min (level + 1, MAX_LEVEL);
		}

		if (player.isHuman) {
			EventManager.TriggerAction (xpEvent, GetXpInfo ());
		}
	}

	public float NextLevelStep (int level)
	{
		return 100 + Mathf.Pow (1 + level, 3);
	}

	public virtual float RetrievedXp ()
	{
		return level * 100 + totalXp / 10;
	}

	public object[] GetXpInfo ()
	{
		return new object[]{ level, totalXp, NextLevelStep (level - 1), NextLevelStep (level) };
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

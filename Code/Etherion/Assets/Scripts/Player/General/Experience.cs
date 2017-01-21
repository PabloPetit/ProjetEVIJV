using UnityEngine;
using System.Collections;

public class Experience : MonoBehaviour
{

	public static int MAX_LEVEL = 20;

	public float[] levelSteps;

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
		xpEvent = new EventName (XpBar.XP_BAR_CHANNEL);
		InitLevelSteps ();
		totalXp = levelSteps [0] + 1f;
		level = 1;
	}

	void InitLevelSteps ()
	{
		levelSteps = new float[MAX_LEVEL + 1];
		for (int i = 0; i <= MAX_LEVEL; i++) {
			levelSteps [i] = NextLevelStep (i);
		}
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


		if (player.isHuman) {
			Debug.Log (xp);
		}


		totalXp += xp;

		level = 0;
		while (level < MAX_LEVEL && totalXp > levelSteps [level]) {
			level += 1;
		}

		if (player.isHuman) {
			EventManager.TriggerAction (xpEvent, GetXpInfo ());
		}
	}

	public float NextLevelStep (int level)
	{
		return 2500f + level * 1200 + Mathf.Pow (4 + level, 3);
	}

	public virtual float RetrievedXp ()
	{
		return  level * 100f + Mathf.Log (totalXp);
	}

	public object[] GetXpInfo ()
	{
		return new object[]{ level, totalXp, levelSteps [level - 1], levelSteps [level] };
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

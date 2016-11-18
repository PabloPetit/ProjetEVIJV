using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour
{
	public static int idCounter = 0;

	public static string LIFE_CHANNEL = "life";
	public static string DAMAGE_CHANNEL = "damage";
	public static string XP_CHANNEL = "xp";
	public static string KILL_COUNT_CHANNEL = "killCount";

	public static int PLAYER_KILL = 0;
	public static int CREATURE_KILL = 1;

	public Team team;

	public int id;
	public int side;
	public string name;

	public Health health;
	public Experience experience;

	public bool isCreature;
	public bool isHuman;

	public EventName killCount;

	public int playerKillCount;
	public int creatureKillCount;


	void Start ()
	{
		id = GetUniqueId ();
		health = GetComponent<Health> ();
		experience = GetComponent<Experience> ();
		OpenChannels ();
		playerKillCount = 0;
		creatureKillCount = 0;
		setCamLightShaft ();
	}

	void setCamLightShaft ()
	{
		if (isHuman) {
			Camera[] cams = GetComponentsInChildren<Camera> ();
			foreach (LightShafts l in FindObjectsOfType<LightShafts> ()) {
				l.m_Cameras = new Camera[]{ cams [0] };
			}
			/*
			Canvas canvas = FindObjectOfType<Canvas> ();
			canvas.worldCamera = cams [1];
			canvas.planeDistance = 0.11f;
		*/
		}

	}

	public void OpenChannels ()
	{
		killCount = new EventName (KILL_COUNT_CHANNEL, id);
		EventManager.StartListening (killCount, KillCount);
		health.OpenChannels (id);
		experience.OpenChannel (id);

	}


	public void CloseChannels ()
	{
		EventManager.StopListening (killCount);
		health.CloseChannels ();
		experience.CloseChannel ();
	}

	public void KillCount (object[] param)
	{
		Player p = (Player)param [0];

		if (p.isCreature) {
			creatureKillCount++;
		} else {
			playerKillCount++;
			team.kills++;
		}

		//Display victim Name
	}


	public static int GetUniqueId ()
	{
		return idCounter++;
	}
}

using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public static int idCounter = 0;

	public static string LIFE_CHANNEL = "life";
	public static string DAMAGE_CHANNEL = "damage";
	public static string XP_CHANNEL = "xp";


	public static int redTeamSide = 1;
	public static int blueTeamSide = 2;
	public static int creatureSide = 3;


	public int id;
	public int side;
	public string name;

	public Health health;
	public Experience experience;

	public bool isHuman;


	void Start ()
	{
		id = GetUniqueId ();
		health = GetComponent<Health> ();
		experience = GetComponent<Experience> ();
		OpenChannels ();
	}

	public void OpenChannels ()
	{
		health.OpenChannels (id);
		experience.OpenChannel (id);
	}

	public void CloseChannels ()
	{
		health.CloseChannels ();
		experience.CloseChannel ();
	}

	public static int GetUniqueId ()
	{
		return idCounter++;
	}
}

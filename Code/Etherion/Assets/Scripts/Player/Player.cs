using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public static int idCounter = 0;

	public static string LIFE_CHANNEL = "life";
	public static string DAMAGE_CHANNEL = "damage";
	public static string XP_CHANNEL = "xp";


	public Team team;

	public int id;
	public int side;
	public string name;

	public Health health;
	public Experience experience;

	public bool isCreature;
	public bool isHuman;

	public int killCount;

	void Start ()
	{
		id = GetUniqueId ();
		health = GetComponent<Health> ();
		experience = GetComponent<Experience> ();
		OpenChannels ();
		killCount = 0;
		setCamLightShaft ();
	}

	void setCamLightShaft ()
	{
		if (isHuman) {
			Camera cam = GetComponentInChildren<Camera> ();
			foreach (LightShafts l in FindObjectsOfType<LightShafts> ()) {
				l.m_Cameras = new Camera[]{ cam };
			}
		}
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

	void Respawn ()
	{
		
	}



	public static int GetUniqueId ()
	{
		return idCounter++;
	}
}

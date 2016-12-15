using UnityEngine;
using System.Collections;

public class SoundThemeSelector : MonoBehaviour
{

	public static string SOUND_THEME_SELECTOR_CHANNEL = "soundThemeSelector";

	public static int DAY = 0;
	public static int NIGHT = 1;
	public static int TRIPOD_UNDETECTED = 2;
	public static int TRIPOD_DETECTED = 3;

	public SkyManager skyManager;

	public AudioSource day;
	public AudioSource night;
	public AudioSource tripodUndetected;
	public AudioSource tripodDetected;

	AudioSource current;

	EventName musicTheme;

	void Start ()
	{
		OpenChannels ();
		
		AddSkyCallBacks ();

		if (skyManager.day) {
			ChangeTheme (new object[]{ 0 });
		} else {
			ChangeTheme (new object[]{ 1 });
		}
			
	}


	void FixedUpdate ()
	{
		//Debug.Log ("Now Playing : "+current.clip.name);
	}

	void AddSkyCallBacks ()
	{
		skyManager.AddDayCallBack (new object[]{ musicTheme, new object[]{ 0 } });
		skyManager.AddNightCallBack (new object[]{ musicTheme, new object[]{ 1 } });
	}

	void OnDisabled ()
	{
		CloseChannels ();
	}

	void OpenChannels ()
	{
		musicTheme = new EventName (SOUND_THEME_SELECTOR_CHANNEL);
		EventManager.StartListening (musicTheme, ChangeTheme);
	}

	void CloseChannels ()
	{
		EventManager.StopListening (musicTheme);
	}

	void ChangeTheme (object[] param)
	{

		int choice = (int)param [0];

		AudioSource next = null;

		if (choice == DAY) {
			next = day;
		} else if (choice == NIGHT) {
			next = night;
		} else if (choice == TRIPOD_UNDETECTED) {
			next = tripodUndetected;
		} else if (choice == TRIPOD_DETECTED) {
			next = tripodDetected;
		}

		//if (current == null){
		//	Debug.Log ("current is null");
		if (current != null) {
			current.Stop ();
		}
		current = next;
		current.Play ();
		Debug.Log ("New Theme : choice : " + choice + " name : " + current.clip.name + " source : " + current.name);
		return;
		//}



		/*
		Debug.Log ("current is NOT null");
		float time = current.time;
		current.Stop ();
		current = next;
		current.time = time;
		current.Play ();
		Debug.Log ("Current id : "+ current.clip.name);
		*/
	}

}

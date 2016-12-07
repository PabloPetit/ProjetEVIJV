using UnityEngine;
using System.Collections;

public class SoundThemeSelector : MonoBehaviour {

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

	void Start(){
		OpenChannels ();
		
		AddSkyCallBacks ();

		if (skyManager.day){
			Debug.Log ("DayTime ! --- On SoundSelector");
			ChangeTheme (new object[]{0});
		}else{
			Debug.Log ("NightTime ! --- On SoundSelector");
			ChangeTheme (new object[]{1});
		}
			
	}


	void AddSkyCallBacks ()
	{
		skyManager.AddDayCallBack (new object[]{musicTheme,new object[]{0}});
		skyManager.AddNightCallBack  (new object[]{musicTheme,new object[]{1}});
	}
		
	void OnDisabled(){
		CloseChannels ();
	}

	void OpenChannels ()
	{
		musicTheme = new EventName (SOUND_THEME_SELECTOR_CHANNEL);
		EventManager.StartListening (musicTheme, ChangeTheme);
	}

	void CloseChannels(){
		EventManager.StopListening (musicTheme);
	}
		
	void ChangeTheme (object[] param)
	{
		int choice = (int)param [0];

		//Debug.Log ("Changing Theme : "+choice);

		AudioSource next = null;

		if (choice == DAY){
			next = day;
		}else if(choice == NIGHT){
			next = night;
		}
		else if(choice == TRIPOD_UNDETECTED){
			next = tripodUndetected;
		}
		else if(choice == TRIPOD_DETECTED){
			next = tripodDetected;
		}
		//Debug.Log ("-------");
		//Debug.Log (current);
		//Debug.Log (next);


		if (current == null){
			current = next;
			//Debug.Log (current);
			current.Play ();
			return;
		}

		float time = current.time;
		current.Stop ();
		current = next;
		current.time = time;
		current.Play ();

	}

}

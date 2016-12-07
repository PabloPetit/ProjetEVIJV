using UnityEngine;
using System.Collections;

public class SoundSystem : MonoBehaviour
{

	public static string BACKGROUND_SOUND_TRIGGER = "backgroundSoundTrigger";
	public static string DAY_SOUND_TRIGGER = "daySoundTrigger";
	public static string NIGHT_SOUND_TRIGGER = "nightSoundTrigger";
	public static string BATTLE_SOUND_TRIGGER = "battleSoundTrigger";


	public AudioSource backround;
	public AudioSource day;
	public AudioSource night;
	public AudioSource battle;

	public SkyManager skyManager;

	EventName backgroundTrigger;
	EventName dayTrigger;
	EventName nightTrigger;
	EventName battleTrigger;


	// Use this for initialization
	void Start ()
	{
		OpenChannels ();
		AddSkyCallBacks ();

		backround.Play ();


		if (skyManager.day)
			day.Play ();
		else
			night.Play ();

	}

	void AddSkyCallBacks ()
	{
		skyManager.AddDayCallBack (dayTrigger);
		skyManager.AddNightCallBack (dayTrigger);
	}

	void OpenChannels ()
	{
		backgroundTrigger = new EventName (BACKGROUND_SOUND_TRIGGER);
		EventManager.StartListening (backgroundTrigger, TriggerBackground);

		dayTrigger = new EventName (DAY_SOUND_TRIGGER);
		EventManager.StartListening (dayTrigger, TriggerDay);

		//nightTrigger = new EventName (NIGHT_SOUND_TRIGGER);
		//EventManager.StartListening (nightTrigger, TriggerNight);

		battleTrigger = new EventName (BATTLE_SOUND_TRIGGER);
		EventManager.StartListening (battleTrigger, TriggerBattle);
	}

	void CloseChannels ()
	{
		EventManager.StopListening (backgroundTrigger);
		EventManager.StopListening (dayTrigger);
		EventManager.StopListening (nightTrigger);
		EventManager.StopListening (battleTrigger);
	}

	void TriggerBackground (object[] param)
	{
		if (backround == null) {
			Debug.Log ("Missing AudioSource in SoundSystem");
			return;
		}
		if (backround.isPlaying) {
			backround.Stop ();
		} else {
			backround.Play ();
		}
	}

	void TriggerDay (object[] param)
	{
		if (day == null || night == null) {
			Debug.Log ("Missing AudioSource in SoundSystem");
			return;
		}
		if (skyManager.day) { 
			StopClipOnNextLoop (night);
			PlayClipOnNextLoop (day);
		} else {
			StopClipOnNextLoop (day);
			PlayClipOnNextLoop (night);
		}
	}

	void TriggerBattle (object[] param)
	{
		if (battle) {
			Debug.Log ("Missing AudioSource in SoundSystem");
			return;
		}
		if (battle.isPlaying) {
			battle.Stop ();
		} else {
			battle.Play ();
		}
	}

	private void PlayClipOnNextLoop (AudioSource clip)
	{
		float delay = backround.clip.length - backround.time;
		StartCoroutine (PlayClip (clip, delay));
		Debug.Log (delay);
	}

	private void StopClipOnNextLoop (AudioSource clip)
	{
		float delay = backround.clip.length - backround.time;
		StartCoroutine (StopClip (clip, delay));
	}

	private IEnumerator PlayClip (AudioSource source, float delay)
	{
		Debug.Log ("New Clip in Queue");
		yield return new WaitForSeconds (delay);
		source.Play ();
		Debug.Log ("Before");
		Debug.Log ("Day : " + day.isPlaying);
		Debug.Log ("Night : " + night.isPlaying);
	}

	private IEnumerator StopClip (AudioSource source, float delay)
	{
		yield return new WaitForSeconds (delay);
		source.Stop ();
	}

}

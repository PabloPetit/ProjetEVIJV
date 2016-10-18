using UnityEngine;
using System.Collections;

public class Saturn5LaunchPanel : Interaction {


	public Saturn5Launch launch;
	AudioSource audio;
	// Use this for initialization
	void Start () {
		launch = transform.parent.parent.parent.Find ("SaturnVRocket").gameObject.GetComponent<Saturn5Launch> ();
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Action(){
		audio.Play ();
		launch.initiated = true;
	}
}

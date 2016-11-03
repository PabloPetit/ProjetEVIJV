using UnityEngine;
using System.Collections;

public class PlayerLight : MonoBehaviour {

	/*
	 * This class controls the character's main light 
	 * It should be Attach to the Playe GameObject
	 * 
	 */

	float timer;
	float timeBetweenToggles = 0.25f;
	Light light;

	void Awake () {
		light = GetComponentInChildren<Light> ();
		timer = 0f;
	}

	void Update () {
		timer += Time.deltaTime;
	}

	public void Toggle(){
		if (timer > timeBetweenToggles) {
			timer = 0f;
			light.enabled = !light.enabled;
		}
	}
}

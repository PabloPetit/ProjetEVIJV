using UnityEngine;
using System.Collections;

public class RotateSky : MonoBehaviour {

	public float speed = 1.2f;

	
	// Update is called once per frame
	void Update () {
		RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BloodCanvas : MonoBehaviour {

	RawImage image;

	// Use this for initialization
	void Start () {
		image = GetComponent<RawImage> ();
		//image.SetNativeSize (Screen.width,Screen.height);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

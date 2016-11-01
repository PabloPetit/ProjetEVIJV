using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HitMarker : MonoBehaviour {

	public float fadeSpeed = 1f;
	RawImage hitMarker;

	void Start () {
		hitMarker = GetComponent<RawImage> ();
	}

	void Update () {
		Color c = hitMarker.color;
		c.a -= fadeSpeed * Time.deltaTime;
		c.a = Mathf.Max (c.a, 0f);
		hitMarker.color = c;
	}

	public void hit(){
		Color c = hitMarker.color;
		c.a = 1f;
		hitMarker.color = c;
	}
}

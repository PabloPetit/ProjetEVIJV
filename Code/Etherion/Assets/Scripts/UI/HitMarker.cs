using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class HitMarker : MonoBehaviour
{

	public float fadeSpeed = 1f;
	RawImage hitMarker;

	private UnityAction hitMarkerListener;

	void Start ()
	{
		hitMarker = GetComponent<RawImage> ();
		hitMarkerListener = new UnityAction (Hit);
	}

	void Update ()
	{
		Color c = hitMarker.color;
		c.a -= fadeSpeed * Time.deltaTime;
		c.a = Mathf.Max (c.a, 0f);
		hitMarker.color = c;
	}

	void Hit ()
	{
		Color c = hitMarker.color;
		c.a = 1f;
		hitMarker.color = c;
	}

	void OnEnable ()
	{
		EventManager.StartListening ("hitMarker", hitMarkerListener); 
	}

	void OnDisable ()
	{
		EventManager.StopListening ("hitMarker", hitMarkerListener);
	}

}

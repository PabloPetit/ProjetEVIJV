using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class HitMarker : MonoBehaviour
{

	public float fadeSpeed = 1f;
	RawImage hitMarker;
	private EventName hitEvent;


	void Awake (){
		hitMarker = GetComponent<RawImage> ();
		hitEvent = new EventName ("hitMarker");
	}

	void Update (){
		Color c = hitMarker.color;
		c.a -= fadeSpeed * Time.deltaTime;
		c.a = Mathf.Max (c.a, 0f);
		hitMarker.color = c;
	}

	void Hit (System.Object param = null){
		Color c = hitMarker.color;
		c.a = 1f;
		hitMarker.color = c;
	}

	void OnEnable (){
		EventManager.StartListening (hitEvent, new System.Action<System.Object> (Hit));
	}

	void OnDisable (){
		EventManager.StopListening (hitEvent);
	}

}

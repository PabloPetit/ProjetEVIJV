using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Test1 : MonoBehaviour
{

	private EventName test;

	void Awake ()
	{
		test = new EventName("test");
	}

	void OnEnable ()
	{
		EventManager.StartListening (test, new System.Action<System.Object> (SomeFunction));
	}

	void OnDisable ()
	{
		EventManager.StopListening (test);
	}

	void SomeFunction (System.Object param)
	{
		float f = (float)param;
		Debug.Log ("Some Function : " + param);
	}
}

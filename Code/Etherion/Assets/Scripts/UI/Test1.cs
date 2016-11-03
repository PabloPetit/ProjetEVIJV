using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Test1 : MonoBehaviour
{

	private UnityAction someListener;

	void Awake ()
	{
		//someListener = new UnityAction (SomeFunction);
	}

	void OnEnable ()
	{
		EventManager.StartListening ("test", new System.Action<float> (SomeFunction));
	}

	void OnDisable ()
	{
		EventManager.StopListening ("test");
	}

	void SomeFunction (float param)
	{
		Debug.Log ("Some Function : " + param);
	}
}

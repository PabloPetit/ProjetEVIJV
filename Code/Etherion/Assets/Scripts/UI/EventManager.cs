using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{

	private Dictionary <string, UnityEvent> eventDictionary;

	private static EventManager eventManager;

	public static EventManager instance {
		get {
			if (!eventManager) {
				eventManager = FindObjectOfType (typeof(EventManager)) as EventManager;
				if (!eventManager) {
					Debug.LogError ("There needs to be one active EventManager script on a GameObject in your scene");
				} else {
					eventManager.Init ();
				}
			}
			return eventManager;
		}
	}

	void Init ()
	{
		if (eventDictionary == null) {
			eventDictionary = new Dictionary<string,UnityEvent> ();
		}
	}

	public static void StartListening (string eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.AddListener (listener);
		} else {
			thisEvent = new UnityEvent ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void StopListening (string eventName, UnityAction listener)
	{
		if (eventManager == null) {
			Debug.LogError ("EventManager not found");
			return;
		}

		UnityEvent thisEvent = null;

		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.RemoveListener (listener);
		} else {
			Debug.LogError ("You are trying to stop listening an event that doesn't exist");
		}
	}

	public static void TriggerEvent (string eventName)
	{
		UnityEvent thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) {
			thisEvent.Invoke ();
		} else {
			Debug.LogError ("You are trying to start listening an event that doesn't exist");
		}
	}
}

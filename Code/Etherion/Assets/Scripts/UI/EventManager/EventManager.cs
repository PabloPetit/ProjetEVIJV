﻿using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;

public class EventManager : MonoBehaviour {

	private Dictionary <EventName, Action<System.Object>> eventDictionary;

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

	void Init (){
		if (eventDictionary == null) {
			eventDictionary = new Dictionary<EventName, Action<System.Object>> ();
		}
	}

	public static void StartListening (EventName name, Action<System.Object> action){
		if (instance.eventDictionary.ContainsKey (name)) {
			Debug.LogError ("The action " + name + " has already been added to the dictionary");
			return;
		} else {
			instance.eventDictionary.Add (name, action);
		}
	}

	public static void StopListening (EventName name){
		
		if (eventManager == null) {
			Debug.LogError ("EventManager not found");
			return;
		}
		instance.eventDictionary.Remove (name);
	}

	public static void TriggerAction (EventName action, System.Object param){
		
		Action<System.Object> a = null;
		if (instance.eventDictionary.TryGetValue (action, out a)) {
			a.Invoke (param);
		}
	}
}
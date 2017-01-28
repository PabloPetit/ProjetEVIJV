using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieScript : MonoBehaviour
{

	public float delay;
	public float lightDelay;

	void Start ()
	{
		foreach (Transform child in transform) {
			Destroy (child.gameObject, t: delay);
		}
		Destroy (gameObject, t: delay);
	}

}

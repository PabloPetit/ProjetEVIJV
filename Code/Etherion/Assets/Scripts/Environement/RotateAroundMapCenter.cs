using UnityEngine;
using System.Collections;

public class RotateAroundMapCenter : MonoBehaviour {

	public float speed = 0.8f;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.RotateAround (Vector3.zero, Vector3.forward, speed * Time.deltaTime);
	}
}

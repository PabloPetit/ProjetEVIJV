using UnityEngine;
using System.Collections;

public class RotateAroundMapCenter : MonoBehaviour {

	public float speed = 0.8f;
	public Vector3 axis = Vector3.forward;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.RotateAround (Vector3.zero, axis, speed * Time.deltaTime);
	}
}

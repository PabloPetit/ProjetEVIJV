using UnityEngine;
using System.Collections;

public class RotateAroundObject : MonoBehaviour {

	public float speed = 0.8f;
	public Vector3 axis = Vector3.forward;
	public GameObject attracter;


	// Update is called once per frame
	void Update () {
		gameObject.transform.RotateAround (attracter.transform.position, axis, speed * Time.deltaTime);
	}
}

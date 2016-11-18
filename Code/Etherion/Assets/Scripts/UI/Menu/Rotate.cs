using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{

	public float speed = 1f;
	public Vector3 axis = Vector3.right;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		// Rotate the object around its local X axis at 1 degree per second
		transform.Rotate (axis * Time.deltaTime * speed);

		// ...also rotate around the World's Y axis
		//transform.Rotate(Vector3.up * Time.deltaTime *3, Space.World);
	
	}
}

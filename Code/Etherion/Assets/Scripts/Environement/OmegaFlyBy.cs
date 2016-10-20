using UnityEngine;
using System.Collections;

public class OmegaFlyBy : MonoBehaviour {

	public float speed = 100f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * speed;
	}
}

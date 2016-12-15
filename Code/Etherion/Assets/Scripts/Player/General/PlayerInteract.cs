using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour {

	public static float RANGE = 4f;

	Ray shootRay;
	RaycastHit shootHit;
	int interactiveMask;
	Camera camera;

	void Start () {
		camera = GetComponentInChildren <Camera> ();
		interactiveMask = LayerMask.GetMask ("Interactive");
	}
		
	public void Interact(){
		shootRay.origin = camera.transform.position;
		shootRay.direction = camera.transform.forward;
		if (Physics.Raycast (shootRay, out shootHit, RANGE, interactiveMask)) {
			Interaction interaction = shootHit.collider.GetComponent<Interaction> ();
			interaction.Interact (gameObject);
		}
	}
}

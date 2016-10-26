using UnityEngine;
using System.Collections;

public class HumanAim : MonoBehaviour {


	public float speed = .0015f;
	public float sideFOV = 60f;
	public float aimFOV = 45f;

	GameObject sidePosition;
	GameObject aimPosition;


	bool aiming;

	Vector3 velocity = Vector3.zero;
	Vector3 aim;
	Vector3 side;

	Camera cam;
	GameObject rightHand;

	void Awake () {
		aiming = false;
		rightHand = transform.Find("Model/Head/RightHand").gameObject;
		sidePosition = transform.Find("Model/Head/SidePosition").gameObject;
		aimPosition = transform.Find("Model/Head/AimPosition").gameObject;
		cam = transform.Find ("Model/Head").gameObject.GetComponent<Camera> ();

		aim = aimPosition.transform.position ;
		side = sidePosition.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

		aim = aimPosition.transform.position ;
		side = sidePosition.transform.position;

		if (aiming){
			rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, aim,  speed);
			cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, aimFOV, speed);
		}else{
			rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, side, speed);
			cam.fieldOfView = Mathf.Lerp (cam.fieldOfView, sideFOV, speed);
		}
	}

	public void Aim(bool pressed){
		aiming = pressed;
	}
}

using UnityEngine;
using System.Collections;

public class HumanAim : MonoBehaviour {


	public float speed = .001f;

	GameObject sidePosition;
	GameObject aimPosition;
	bool aiming;

	Vector3 velocity = Vector3.zero;


	GameObject rightHand;

	void Awake () {
		aiming = false;
		rightHand = transform.Find("Model/Head/RightHand").gameObject;
		sidePosition = transform.Find("Model/Head/SidePosition").gameObject;
		aimPosition = transform.Find("Model/Head/AimPosition").gameObject;

	}
	
	// Update is called once per frame
	void Update () {


		Vector3 aim = aimPosition.transform.position;
		Vector3 side = sidePosition.transform.position;

		if (aiming){

			//rightHand.transform.position = Vector3.Lerp(side, aim, Time.deltaTime * speed);

			rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, aim,  speed);
			//rightHand.transform.position = Vector3.SmoothDamp(side, aim, ref velocity, 3f);
			//rightHand.transform.position = Vector3.MoveTowards(side,aim, 0f);
		}else{

			//rightHand.transform.position = Vector3.Lerp(aim, side, Time.deltaTime * speed);

			rightHand.transform.position = Vector3.Lerp(rightHand.transform.position, side, speed);
			//rightHand.transform.position = Vector3.SmoothDamp(aim, side, ref velocity, 3f);
			//rightHand.transform.position = Vector3.MoveTowards(aim, side, 0f);
		}
	}

	public void Aim(bool pressed){
		aiming = pressed;
	}
}

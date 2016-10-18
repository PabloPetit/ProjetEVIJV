using UnityEngine;
using System.Collections;

public class SkyboxCameraRotation : MonoBehaviour {

	// Use this for initialization
	float turnValue = 0f;

	void Start () {
	
	}
	
	void LateUpdate(){

		turnValue += Time.deltaTime;
		Vector3 rotationValue = new Vector3 (Camera.main.transform.rotation.eulerAngles.x,
			                        Camera.main.transform.rotation.eulerAngles.y + turnValue,
			                        Camera.main.transform.rotation.eulerAngles.z);

		transform.rotation = Quaternion.Euler (rotationValue);

	}






}

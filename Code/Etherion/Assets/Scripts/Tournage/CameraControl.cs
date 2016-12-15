using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CameraControl : MonoBehaviour
{

	public float speed;

	public float accel;

	public float walkSpeed;
	public float runSpeed;

	public float XSensitivity = 2f;
	public float YSensitivity = 2f;
	public bool clampVerticalRotation = true;
	public float MinimumX = -90F;
	public float MaximumX = 90F;
	public bool smooth;
	public float smoothTime = 5f;

	private Quaternion m_CharacterTargetRot;
	private Quaternion m_CameraTargetRot;

	Camera cam;
	private bool m_cursorIsLocked = true;
	public bool lockCursor = true;


	void Start ()
	{

		cam = GetComponentInChildren<Camera> ();
		Init (gameObject.transform, cam.transform);

		foreach (LightShafts l in FindObjectsOfType<LightShafts> ()) {
			l.m_Cameras = new Camera[]{ cam };
		}

	}

	public void Init (Transform character, Transform camera)
	{
		m_CharacterTargetRot = character.localRotation;
		m_CameraTargetRot = camera.localRotation;
	}

	void Update ()
	{
		LookRotation (gameObject.transform, cam.transform);

		Vector3 dir = Vector3.zero;
		bool p = false;

		if (Input.GetKey (KeyCode.Z)) {
			dir += cam.transform.forward;
			p = true;
		}
		if (Input.GetKey (KeyCode.S)) {
			dir -= cam.transform.forward;
			p = true;
		}
		if (Input.GetKey (KeyCode.Q)) {
			dir -= cam.transform.right;
			p = true;
		}
		if (Input.GetKey (KeyCode.D)) {
			dir += cam.transform.right;
			p = true;
		}

		if (p) {
			if (Input.GetKey (KeyCode.LeftShift)) {
				speed = Mathf.Min (speed + Time.deltaTime * accel, runSpeed);
			} else if (dir.magnitude > .1) {
				speed = Mathf.Max (speed - Time.deltaTime * accel, walkSpeed);
			} 
		} else {
			speed = Mathf.Max (speed - Time.deltaTime * accel, 0f);
			dir = cam.transform.forward;
		}
			
		gameObject.transform.position += dir * speed * Time.deltaTime;

	}

	public void LookRotation (Transform character, Transform camera)
	{
		float yRot = CrossPlatformInputManager.GetAxis ("Mouse X") * XSensitivity;
		float xRot = CrossPlatformInputManager.GetAxis ("Mouse Y") * YSensitivity;

		m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
		m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);

		if (clampVerticalRotation)
			m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);

		if (smooth) {
			character.localRotation = Quaternion.Slerp (character.localRotation, m_CharacterTargetRot,
				smoothTime * Time.deltaTime);
			camera.localRotation = Quaternion.Slerp (camera.localRotation, m_CameraTargetRot,
				smoothTime * Time.deltaTime);
		} else {
			character.localRotation = m_CharacterTargetRot;
			camera.localRotation = m_CameraTargetRot;
		}
		UpdateCursorLock ();
			
	}

	Quaternion ClampRotationAroundXAxis (Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;

		float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);

		angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);

		q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);

		return q;
	}

	public void SetCursorLock (bool value)
	{
		lockCursor = value;
		if (!lockCursor) {//we force unlock the cursor if the user disable the cursor locking helper
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	public void UpdateCursorLock ()
	{
		//if the user set "lockCursor" we check & properly lock the cursos
		if (lockCursor)
			InternalLockUpdate ();
	}

	private void InternalLockUpdate ()
	{
		if (Input.GetKeyUp (KeyCode.Escape)) {
			m_cursorIsLocked = false;
		} else if (Input.GetMouseButtonUp (0)) {
			m_cursorIsLocked = true;
		}

		if (m_cursorIsLocked) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		} else if (!m_cursorIsLocked) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}

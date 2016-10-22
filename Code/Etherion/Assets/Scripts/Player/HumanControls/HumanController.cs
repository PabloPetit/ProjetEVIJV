using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class HumanController : MonoBehaviour {

	CharacterController characterController;
	Camera camera;
	MouseLook mouseLook;

	PlayerWeapon weapon;
	PlayerLight light;
	PlayerInteract interact;
	PlayerBuffs buffs;
	PlayerTraps traps;
	PlayerAbility1 ability1;
	HumanAim aim;

	public float walkSpeed;
	public float runSpeed;
	public float jumpForce;
	public float jumpTime;
	public float stickToGroundForce;

	Animator animator;


	float jumpTimer;
	bool IsWalking;
	bool IsRunning;
	bool IsJumping;
	float verticalSpeed;

	void Awake(){

		characterController = GetComponent<CharacterController> ();
		camera = Camera.main;
		mouseLook = new MouseLook ();
		mouseLook.Init(transform , camera.transform);

		weapon = gameObject.GetComponent<PlayerWeapon> ();
		light = gameObject.GetComponent<PlayerLight> ();
		interact = gameObject.GetComponent<PlayerInteract> ();
		buffs = gameObject.GetComponent<PlayerBuffs> ();
		traps = gameObject.GetComponent<PlayerTraps> ();
		ability1 = gameObject.GetComponent<PlayerAbility1> ();
		aim = gameObject.GetComponent<HumanAim> ();

		animator = GetComponentInChildren<Animator> ();

		IsWalking = false;
		IsRunning = false;
		IsJumping = false;
	}

	void Update () {
		RotateView();
		Actions();
	}

	void FixedUpdate(){
		jumpTimer += Time.fixedDeltaTime;
		Movement ();
	}
		

	void Actions(){


		if (Input.GetKey (KeyMap.fire)) {
			weapon.Shoot ();
		}

		aim.Aim (Input.GetKey (KeyMap.aim));


		if (Input.GetKey (KeyMap.light)) {
			light.Toggle ();
		}
		if (Input.GetKeyDown (KeyMap.interact)) {
			/* Note : here we use GetKeyDown instead of GetKey */
			interact.Interact ();
		}
		if (Input.GetKey (KeyMap.buffLife)) {

		}
		if (Input.GetKey (KeyMap.buffSpeed)) {

		}
		if (Input.GetKey (KeyMap.buffStrenght)) {

		}
		if (Input.GetKey (KeyMap.ability1)) {

		}
		if (Input.GetKey (KeyMap.ability2)) {

		}
			
	}
		

	void Movement(){

		Vector3 desiredMove = Vector3.zero;
		Vector3 moveDir = Vector3.zero;

		IsWalking = false;
		IsRunning = false;
		bool jump = false;

		if (characterController.isGrounded || jumpTimer > jumpTime) {
			IsJumping = false;
		}

		if (Input.GetKey (KeyMap.forward)) {
			desiredMove += camera.transform.forward;
			IsWalking = true;
		}
		if (Input.GetKey (KeyMap.backward)) {
			desiredMove -= camera.transform.forward;
			IsWalking = true;
		}
		if (Input.GetKey (KeyMap.right)) {
			desiredMove += camera.transform.right;
			IsWalking = true;
		}
		if (Input.GetKey (KeyMap.left)) {
			desiredMove -= camera.transform.right;
			IsWalking = true;
		}

		if (IsWalking) {
			if (Input.GetKey (KeyMap.run)) {
				IsWalking = false;
				IsRunning = true;
				animator.SetTrigger ("StartRun");
			} else {
				animator.SetTrigger ("StartWalk");
			}
		} else {
			animator.SetTrigger ("Stopped");
		}

		if (!IsJumping && Input.GetKey (KeyMap.jump) && characterController.isGrounded) {
			IsJumping = true;
			jumpTimer = 0;
		}

		RaycastHit hitInfo;
		Physics.SphereCast(transform.position, characterController.radius, Vector3.down, out hitInfo,
			characterController.height/2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
		
		desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

		if (IsWalking) {
			moveDir.x = desiredMove.x * walkSpeed;
			moveDir.z = desiredMove.z * walkSpeed;
		}
		if (IsRunning) {
			moveDir.x = desiredMove.x * runSpeed;
			moveDir.z = desiredMove.z * runSpeed;
		}

		verticalSpeed = Mathf.Max (jumpForce * (jumpTime - jumpTimer), stickToGroundForce);
			
		if (IsJumping) {
			
			moveDir.y = + verticalSpeed;
		} else {
			moveDir.y = - verticalSpeed;
		}


			
		characterController.Move(moveDir*Time.fixedDeltaTime);
	}



	private void RotateView()
	{
		mouseLook.LookRotation (transform, camera.transform);
	}

}

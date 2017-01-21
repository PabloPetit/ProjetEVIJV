using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class HumanController : MonoBehaviour
{


	public static float footstepVolume = .05f;
	public static float JET_PACK_MULMTIPLIER = 60f;

	public static float MAX_JUMP_ANGLE = 50f;


	CharacterController characterController;
	Camera camera;
	MouseLook mouseLook;

	PlayerWeapon weapon;
	PlayerLight light;
	PlayerInteract interact;
	PlayerBuffs buffs;
	PlayerTraps traps;
	PlayerAbility1 ability1;
	PlayerHealth health;
	PlayerGrenade grenade;
	HumanAim aim;

	public float walkSpeed;
	public float runSpeed;
	public float jumpForce;
	public float jumpTime;
	public float stickToGroundForce;

	public float jetPackImpulse;
	public float jetPackTime;
	public float jetPackCoolDown;

	float jetPackTimer;
	bool jetPacking;

	public float dashImpulse;
	public float dashCoolDown;
	public float dashTime;
	float dashTimer;
	Vector3 dashDirection;

	Animator animator;

	public bool IsWalking;
	public bool IsRunning;
	public bool IsJumping;
	Vector3 airDirection;
	float verticalSpeed;
	float jumpTimer;

	public AudioClip[] footsteps;
	AudioSource audio;
	float footstepsInterval = .5f;
	float timer;

	float groundAngle = 0f;

	void Awake ()
	{

		characterController = GetComponent<CharacterController> ();
		camera = Camera.main;
		mouseLook = new MouseLook ();
		mouseLook.Init (transform, camera.transform);

		weapon = gameObject.GetComponent<PlayerWeapon> ();
		light = gameObject.GetComponent<PlayerLight> ();
		interact = gameObject.GetComponent<PlayerInteract> ();
		buffs = gameObject.GetComponent<PlayerBuffs> ();
		traps = gameObject.GetComponent<PlayerTraps> ();
		ability1 = gameObject.GetComponent<PlayerAbility1> ();
		health = gameObject.GetComponent<PlayerHealth> ();
		aim = gameObject.GetComponent<HumanAim> ();
		grenade = GetComponent<PlayerGrenade> ();

		animator = GetComponentInChildren<Animator> ();
		audio = GetComponentInChildren<AudioSource> ();


		IsWalking = false;
		IsRunning = false;
		IsJumping = false;

		verticalSpeed = 0f;
		timer = 0f;
		jetPackTimer = jetPackTime;
	}

	void Update ()
	{
		RotateView ();

		if (health.dead) {
			return;
		}

		timer += Time.deltaTime;
		Actions ();
		FootSteps ();
		jetPackManagement ();
		dashManagement ();
	}


	void FixedUpdate ()
	{
		if (health.dead) {
			return;
		}
		jumpTimer += Time.fixedDeltaTime;
		Movement ();
	}

	void jetPackManagement ()
	{
		jetPackTimer += Time.deltaTime;

		if (Input.GetKeyDown (KeyMap.jetPack) && jetPackTimer > jetPackCoolDown && !jetPacking && !IsJumping) {
			jetPacking = true;
			jetPackTimer = 0f;
			Vector3 v = characterController.velocity;
			v.y = 0f;
			airDirection += v * 2f;
			timer = 0f;

		} else if (jetPacking && jetPackTimer >= jetPackTime) {
			jetPacking = false;
			jetPackTimer = 0f;
		}
	}

	void dashManagement ()
	{

		dashTimer += Time.deltaTime;

		if (dashDirection.Equals (Vector3.zero)) {
			if (dashTimer > dashCoolDown) {
				if (Input.GetKeyDown (KeyMap.leftDash)) {
					dashTimer = 0f;
					dashDirection = -camera.transform.right * dashImpulse;
				} else if (Input.GetKeyDown (KeyMap.rightDash)) {
					dashTimer = 0f;
					dashDirection = camera.transform.right * dashImpulse;
				}
			}
		} else if (dashTimer > dashTime) {
			dashDirection = Vector3.zero;
		}
	
	}


	void FootSteps ()
	{
		if (footsteps.Length == 0 || IsJumping || !characterController.isGrounded)
			return;
		if ((IsWalking && timer > footstepsInterval) || (IsRunning && timer > footstepsInterval / 2f)) {
			audio.PlayOneShot (footsteps [(int)Random.Range (0, footsteps.Length)], footstepVolume);
			timer = 0f;
		}
	}

	void Actions ()
	{

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

		if (Input.GetKeyDown (KeyMap.grenade)) {
			grenade.ThrowGrenade ();
		}

		if (Input.GetKey (KeyCode.I)) {
			walkSpeed = 20;
			runSpeed = 200;
			jumpForce = 40;
		}
		if (Input.GetKey (KeyCode.K)) {
			walkSpeed = 8;
			runSpeed = 20;
			jumpForce = 12;
		}
			
	}

	void OnControllerColliderHit (ControllerColliderHit hit)
	{
		groundAngle = Vector3.Angle (Vector3.up, hit.normal);
	}



	void Movement ()
	{

		Vector3 desiredMove = Vector3.zero;
		Vector3 moveDir = Vector3.zero;

		bool IsWalkingTmp = false;
		bool jump = false;

		if (jumpTimer > jumpTime) {
			IsJumping = false;
			airDirection = Vector3.zero;
		}

		if (Input.GetKey (KeyMap.forward)) {
			desiredMove += camera.transform.forward;
			IsWalkingTmp = true;
		}
		if (Input.GetKey (KeyMap.backward)) {
			desiredMove -= camera.transform.forward;
			IsWalkingTmp = true;
		}
		if (Input.GetKey (KeyMap.right)) {
			desiredMove += camera.transform.right / 2f;
			IsWalkingTmp = true;
		}
		if (Input.GetKey (KeyMap.left)) {
			desiredMove -= camera.transform.right / 2f;
			IsWalkingTmp = true;
		}

		if (IsWalkingTmp) {
			if (Input.GetKey (KeyMap.run) && !Input.GetKey (KeyMap.aim)) {

				if (!IsRunning) {
					animator.SetTrigger ("Run");
				}
				IsWalking = false;
				IsRunning = true;
			} else {
				if (!IsWalking) {
					animator.SetTrigger ("Walk");
				}
				IsWalking = true;
				IsRunning = false;
			}
		} else if (IsWalking || IsRunning) {
			animator.SetTrigger ("Idle");
			IsWalking = false;
			IsRunning = false;
		}

		if (!IsJumping && Input.GetKey (KeyMap.jump) && characterController.isGrounded && groundAngle < MAX_JUMP_ANGLE) {
			IsJumping = true;
			jumpTimer = 0;
			airDirection = characterController.velocity;
			airDirection.y = 0f;

		}

		RaycastHit hitInfo;
		Physics.SphereCast (transform.position, characterController.radius, Vector3.down, out hitInfo,
			characterController.height / 2f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
		
		desiredMove = Vector3.ProjectOnPlane (desiredMove, hitInfo.normal).normalized;

		if (IsWalking) {
			moveDir.x = desiredMove.x * walkSpeed;
			moveDir.z = desiredMove.z * walkSpeed;
		}
		if (IsRunning) {
			moveDir.x = desiredMove.x * runSpeed;
			moveDir.z = desiredMove.z * runSpeed;
		}
	
		if (IsJumping) {
			verticalSpeed = jumpForce;
			moveDir += camera.transform.forward;

		} else if (jetPacking) {
			verticalSpeed = jetPackImpulse;
			moveDir += camera.transform.forward * JET_PACK_MULMTIPLIER;
		} else if (!characterController.isGrounded) {
			verticalSpeed -= stickToGroundForce * Time.fixedDeltaTime;
		} else {
			verticalSpeed = 0f;
		}

		verticalSpeed = Mathf.Max (verticalSpeed, -stickToGroundForce * 3f);
		moveDir.y = verticalSpeed;

		moveDir += dashDirection;

		characterController.Move (moveDir * Time.fixedDeltaTime);
	}

	private void RotateView ()
	{
		mouseLook.LookRotation (transform, camera.transform);
	}

}

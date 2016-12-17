using UnityEngine;
using System.Collections;
using UnityStandardAssets.Utility;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Characters.FirstPerson;

public class HumanController : MonoBehaviour
{

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
	HumanAim aim;

	public float walkSpeed;
	public float runSpeed;
	public float jumpForce;
	public float jumpTime;
	public float stickToGroundForce;

	Animator animator;

	public bool IsWalking;
	public bool IsRunning;
	public bool IsJumping;
	float verticalSpeed;
	float jumpTimer;

	public AudioClip[] footsteps;
	AudioSource audio;
	float footstepsInterval = .5f;
	float timer;

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

		animator = GetComponentInChildren<Animator> ();
		audio = GetComponentInChildren<AudioSource> ();


		IsWalking = false;
		IsRunning = false;
		IsJumping = false;

		verticalSpeed = 0f;
		timer = 0f;
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
	}

	void FixedUpdate ()
	{
		if (health.dead) {
			//return;
		}
		jumpTimer += Time.fixedDeltaTime;
		Movement ();
	}


	void FootSteps ()
	{
		if (footsteps.Length == 0 || IsJumping || !characterController.isGrounded)
			return;
		if ((IsWalking && timer > footstepsInterval) || (IsRunning && timer > footstepsInterval / 2f)) {
			audio.PlayOneShot (footsteps [(int)Random.Range (0, footsteps.Length)], .3f);
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

		if (Input.GetKey (KeyCode.I)) {
			walkSpeed = 20;
			runSpeed = 200;
			jumpForce = 130;
		}
		if (Input.GetKey (KeyCode.K)) {
			walkSpeed = 8;
			runSpeed = 20;
			jumpForce = 12;
		}
			
	}


	void Movement ()
	{

		Vector3 desiredMove = Vector3.zero;
		Vector3 moveDir = Vector3.zero;

		bool IsWalkingTmp = false;
		bool jump = false;

		if (jumpTimer > jumpTime) {
			IsJumping = false;
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
			desiredMove += camera.transform.right;
			IsWalkingTmp = true;
		}
		if (Input.GetKey (KeyMap.left)) {
			desiredMove -= camera.transform.right;
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

		if (!IsJumping && Input.GetKey (KeyMap.jump) && characterController.isGrounded) {
			IsJumping = true;
			jumpTimer = 0;
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
		} else if (!characterController.isGrounded) {
			verticalSpeed -= stickToGroundForce * Time.fixedDeltaTime;
		} else {
			verticalSpeed = 0f;
		}

		verticalSpeed = Mathf.Max (verticalSpeed, -stickToGroundForce * 3f);
		moveDir.y = verticalSpeed;

		characterController.Move (moveDir * Time.fixedDeltaTime);
	}

	private void RotateView ()
	{
		mouseLook.LookRotation (transform, camera.transform);
	}

}

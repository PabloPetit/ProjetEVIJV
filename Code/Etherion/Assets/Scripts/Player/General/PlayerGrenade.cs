using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrenade : MonoBehaviour
{

	public GameObject grenadePrefab;

	public float coolDown;

	public bool friendlyFire;

	public float initialDamage;

	public float minDamage;

	public float damageDecrease;


	float timer;

	CharacterController characterController;
	Camera camera;
	Player player;

	// Use this for initialization
	void Start ()
	{
		player = GetComponent<Player> ();
		characterController = GetComponent<CharacterController> ();
		camera = Camera.main;

	}

	void Update ()
	{
		timer += Time.deltaTime;
	}

	public void ThrowGrenade ()
	{
		if (timer > coolDown && player.experience.level > 4) {
			Vector3 pos = camera.transform.position + camera.transform.forward * 2f;
			Vector3 velocity = characterController.velocity + camera.transform.forward * 50f;
			PlasmaGrenade.Create (grenadePrefab, pos, velocity, player, friendlyFire, initialDamage, minDamage, damageDecrease);
			timer = 0f;
		}

	}

}

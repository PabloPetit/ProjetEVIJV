using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripodWeaponSystem : MonoBehaviour
{


	TripodController trip;

	public float angularSpeed;


	// Timestamps are better than timers

	//Prefabs;

	public GameObject laserPrefab;
	public GameObject subBombPrefab;
	public GameObject subBombAmmoPrefab;
	public GameObject closeRangeEffectPrefab;


	//CloseRangeAttack

	public float cRDamage;
	public float cRCoolDown;
	public float cRDetector;

	float cRLastUse;

	//Lasers

	public float laserDamage;
	public float laserZoneRadius;
	public float laserCoolDown;

	float laserLastUse;

	//SubAmmoBomb

	public float subAmmoNumberOfSubMunitions;
	public float subAmmoCoolDown;
	float subAmmoLastUse;

	GameObject target;

	void Start ()
	{
		laserLastUse = Time.time;
		subAmmoLastUse = Time.time;
		cRLastUse = Time.time;
		trip = GetComponent<TripodController> ();
	}


	void Update ()
	{
		AimAtTarget ();
	}

	void AimAtTarget ()
	{ // Don't forget to unactive
		if (target == null)
			return;
		Vector3 direction = (target.transform.position - transform.position).normalized;
		direction = Vector3.RotateTowards (transform.forward, direction, angularSpeed * Time.fixedDeltaTime, 0.0f);
		direction.y = 0f;

		//

		transform.rotation = Quaternion.LookRotation (direction);
		direction = (target.transform.position - trip.barrel.transform.position).normalized;
		direction = Vector3.RotateTowards (trip.barrel.transform.forward, direction, angularSpeed * Time.fixedDeltaTime, 0.0f);
		trip.barrel.transform.rotation = Quaternion.LookRotation (direction);

	}

	public void ShootLasers ()
	{
		
	}

	public void CloseRangeAttack ()
	{


	}

	public void ShootSubAmmo ()
	{
		//Aim for 50 meters above the target, fire and forget
	}


	public void SetTarget (GameObject newTarget)
	{
		target = newTarget;
	}

}

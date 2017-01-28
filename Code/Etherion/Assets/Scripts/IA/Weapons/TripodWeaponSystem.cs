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
	public GameObject closeRangePrefab;


	//CloseRangeAttack

	public float cRDamage;
	public float cRCoolDown;
	public float cRDetector;

	float cRLastUse;

	//Lasers

	public float laserDamage;
	public float laserZoneRadius;
	public float laserCoolDown;
	public float laserSpeed;
	public float laserGuidanceStart;
	public float laserGuidanceMaxDeviation;

	float laserLastUse;

	//SubAmmoBomb

	public float subAmmoNumberOfSubMunitions;
	public float subAmmoCoolDown;
	public float subAmmoMinDist;
	float subAmmoLastUse;


	GameObject closeRangeBarrel;
	public GameObject target;

	void Start ()
	{
		laserLastUse = Time.time;
		subAmmoLastUse = Time.time;
		cRLastUse = Time.time;
		trip = GetComponent<TripodController> ();
		closeRangeBarrel = transform.Find ("hips/CloseRangeBarrel").gameObject;
	}


	void Update ()
	{
		//AimAtTarget ();
	}

	void AimAtTarget ()
	{ // Don't forget to unactive
		if (target == null)
			return;
		Vector3 direction = (target.transform.position - transform.position).normalized;
		direction = Vector3.RotateTowards (transform.forward, direction, angularSpeed * Time.fixedDeltaTime, 0.0f);
		direction.y = 0f;


		transform.rotation = Quaternion.LookRotation (direction);
		direction = (target.transform.position - trip.barrel.transform.position).normalized;
		direction = Vector3.RotateTowards (trip.barrel.transform.forward, direction, angularSpeed * Time.fixedDeltaTime, 0.0f);
		trip.barrel.transform.rotation = Quaternion.LookRotation (direction);

	}

	public void ShootLasers ()
	{
		if (isLaserAttackReady () && target != null) {

			AutoGuidedBullet.Create (laserPrefab, trip.barrel.transform, laserSpeed, 0f, laserDamage, laserDamage, 0f, true, trip.player, target, laserGuidanceStart, laserGuidanceMaxDeviation);

			laserLastUse = Time.time;
		}
	}

	public void CloseRangeAttack ()
	{
		if (isCloseRangeReady ()) {

			Vector3 pos = closeRangeBarrel.transform.position;
			Vector3 velocity = Vector3.down;
			TripodCloseRangeGrenade.Create (closeRangePrefab, pos, velocity, trip.player, false, cRDamage, cRDamage, 0);


			cRLastUse = Time.time;
		}

	}

	public void ShootSubAmmoAttack ()
	{

		if (isSubAmmoAttackReady () && target != null) {

			Player p = null;

			float bestDist = trip.maxAimingDistance + 1f;

			foreach (Player p2 in trip.enemiesAround) {
				float dist = Vector3.Distance (trip.transform.position, p2.transform.position);

				if (dist > trip.weaponSystem.subAmmoMinDist && dist < bestDist) {
					p = p2;
					bestDist = dist;
					break;
				}
			}

			if (p != null) {



				subAmmoLastUse = Time.time;
			}




		}
	}


	public void SetTarget (GameObject newTarget)
	{
		target = newTarget;
	}

	public bool isCloseRangeReady ()
	{
		return Time.time - cRLastUse > cRCoolDown;
	}

	public bool isLaserAttackReady ()
	{
		return Time.time - laserLastUse > laserCoolDown;
	}

	public bool isSubAmmoAttackReady ()
	{
		return Time.time - subAmmoLastUse > subAmmoCoolDown;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripodWeaponSystem : MonoBehaviour
{


	TripodController trip;


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




	void Start ()
	{
		laserLastUse = Time.time;
		subAmmoLastUse = Time.time;
		cRLastUse = Time.time;
	}


	public void ShootLasers ()
	{
		
	}


}

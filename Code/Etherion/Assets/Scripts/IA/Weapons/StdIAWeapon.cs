using UnityEngine;
using System.Collections;

public class StdIAWeapon : MonoBehaviour {


	public float initialDamage;
	public float range;
	public float dispertion;
	public float speed;
	public float damageDecrease;
	public float minDamage;

	public bool overLoaded;
	public float overloadIncrement;
	public float overloadThreshold;
	public float overloadVal;
	public float overloadCoolSpeed;

	public float timer;

	public float timeBetweenBullets;

	Player player;

	public GameObject bulletPrefab;
	public GameObject barrel;

	AudioSource gunShot;

	public void Start(){
		this.player = GetComponent<Player> ();
		gunShot = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		DecreaseOverload ();
	}

	public void Shoot ()
	{

		// TODO: Overload
		if (timer >= timeBetweenBullets && Time.timeScale != 0 && !overLoaded) { 

			timer = 0f;

			Action ();

			IncreaseOverload ();

		}
	}

	public void Action(){
		Bullet.Create (bulletPrefab, barrel.transform, speed, dispertion, initialDamage, minDamage, damageDecrease, false, player);
		gunShot.Play ();
	}

	public void IncreaseOverload ()
	{
		overloadVal += overloadIncrement;
		if (overloadVal > overloadThreshold) {
			overLoaded = true;
		}
	}

	public void DecreaseOverload ()
	{
		overloadVal = Mathf.Max (0f, overloadVal - overloadCoolSpeed * Time.deltaTime);
		if (overLoaded && overloadVal == 0f) {
			overLoaded = false;
		}

	}

}

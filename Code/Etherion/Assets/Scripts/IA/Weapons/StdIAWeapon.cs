using UnityEngine;
using System.Collections;

public class StdIAWeapon : MonoBehaviour
{


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

	float mult = 1f;

	public void Start ()
	{
		this.player = GetComponent<Player> ();
		gunShot = GetComponent<AudioSource> ();

	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;
		mult = 1 + Mathf.Log10 (.5f + player.experience.level / 2f);
		DecreaseOverload ();
	}

	public void Shoot ()
	{

		// TODO: Overload
		if (timer >= timeBetweenBullets * 1 / mult && Time.timeScale != 0 && !overLoaded) { 

			timer = 0f;

			Action ();
			IncreaseOverload ();

		}
	}

	public void Action ()
	{
		float mult = 1 + Mathf.Log10 (.5f + player.experience.level / 2f);
		int plus = GameManager.iaLevel * 2;
		Bullet.Create (bulletPrefab, barrel.transform, speed, dispertion, plus + initialDamage * mult, minDamage * mult, damageDecrease, false, player);
		gunShot.Play ();
	}

	public void IncreaseOverload ()
	{
		overloadVal += overloadIncrement / mult;
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

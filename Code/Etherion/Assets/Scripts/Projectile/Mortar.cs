using UnityEngine;
using System.Collections;

public class Mortar : BalisticShot, IDamage
{

	public Player shooter { get ; set ; }

	public bool hitMarker { get ; set ; }

	public bool friendlyFire { get ; set ; }

	public float initialDamage { get ; set ; }

	public float minDamage { get ; set ; }

	public float damageDecrease { get ; set ; }

	public float radius;

	public GameObject[] accelerationEffects;
	public GameObject[] explosionEffects;


	void Start ()
	{
		base.Start ();
		this.renderer = transform.Find ("missile").GetComponent<MeshRenderer> ();
	}

	public static GameObject Create (GameObject prefab, Transform barrel, float speed, float dispertion, float acceleration, float ascendingTime, float maxDescendingSpeed, float deathDelay,
	                                 float initialDamage, float minDamage, float damageDecrease, bool friendlyFire, bool hitMarker, Player shooter, float radius)
	{
		GameObject pro = BalisticShot.Create (prefab, barrel, speed, dispertion, acceleration, ascendingTime, maxDescendingSpeed, deathDelay);
		Mortar mortar = pro.GetComponent<Mortar> ();

		mortar.initialDamage = initialDamage;
		mortar.minDamage = minDamage;
		mortar.damageDecrease = damageDecrease;
		mortar.friendlyFire = friendlyFire;
		mortar.hitMarker = hitMarker;
		mortar.shooter = shooter;
		mortar.radius = radius;

		return pro;
	}


	protected void StartDescent ()
	{
		base.StartDescent ();
		foreach (GameObject go in accelerationEffects) {
			go.SetActive (false);
		}
	}

	protected override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);
		foreach (GameObject go in explosionEffects) {
			go.SetActive (true);
		}

		Damage.DoZoneDamage (shooter, transform, radius, this);
	}

		
}

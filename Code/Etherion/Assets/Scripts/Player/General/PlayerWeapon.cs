using UnityEngine;
using System.Collections;

public class PlayerWeapon : MonoBehaviour {

	public float damagePerShot;
	public float timeBetweenBullets;
	public float range;
	public float effectsDisplayTime;
	public float overloadTime;
	public bool overLoaded;
	public float timer;



	public virtual void EnableEffects(Vector3 start, Vector3 end){
	}
	public virtual void DisableEffects (){
	}

}

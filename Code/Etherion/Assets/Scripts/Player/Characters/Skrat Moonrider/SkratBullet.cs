using UnityEngine;
using System.Collections;

public class SkratBullet : Projectile {


	void Start () {
		initialDamage = 2f;
		damageDecrease = 0f;
		speed = 500f;
		range = 1000f;
	}


}

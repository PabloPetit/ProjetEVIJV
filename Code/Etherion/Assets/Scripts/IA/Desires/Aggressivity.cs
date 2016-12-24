using UnityEngine;
using System.Collections;

public class Aggressivity : Desire {

	public static float STD_MIN_VALUE = 25f;

	public static float ENEMY_IN_SIGHT_MULTIPLIER = 1;

	public float SHOT_VALUE = 20f;

	Player lastShooter;
	float lastHit;

	public Aggressivity(IA ia, Player player) : base(ia, player){
		
	}


	public override void Setup (){
		lastShooter = null;
		lastHit = 0f;
		this.MIN_VALUE = STD_MIN_VALUE * personalCoeff;
	}

	//Agressivity Goes 
	public override void Update (){
		Decrease ();
		CheckShot ();
		CheckEnemiInSight ();
		CheckCurrentScore ();

		value = Mathf.Max (MIN_VALUE, Mathf.Min (MAX_VALUE, value));
	}

	public void CheckShot(){
		if (lastShooter != player.health.lastShooter || lastHit != player.health.lastHitDate){
			lastHit = player.health.lastHitDate;
			lastShooter = player.health.lastShooter;
			value += personalCoeff * SHOT_VALUE;
		}
	}

	public void CheckEnemiInSight(){
		if (ia.closestEnemy != null){
			float dist = Vector3.Distance (ia.closestEnemy.gameObject.transform.position, player.gameObject.transform.position);
			value += personalCoeff * (ia.maxAimingDistance / (dist + 1)) * ENEMY_IN_SIGHT_MULTIPLIER * Time.deltaTime;
			//Some analysis must be made to compare decrease and increase values
		}
	}

	public void CheckCurrentScore(){}
		
}

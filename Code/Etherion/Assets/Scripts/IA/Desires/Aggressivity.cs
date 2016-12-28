using UnityEngine;
using System.Collections;

public class Aggressivity : Desire {

	public static string NAME = "AGGRESSIVITY";

	public static float STD_MIN_VALUE = 25f;

	public static float ENEMY_IN_SIGHT_MULTIPLIER = 1;

	public float SHOT_VALUE = 20f;

	public static float FORGET_DELAY = 5f;

	Player lastShooter;
	float lastHit;

	float timer;

	public Aggressivity(IA ia) : base(ia){
		timer = 0f;
	}


	public override void Setup (){
		lastShooter = null;
		lastHit = 0f;
		this.MIN_VALUE = STD_MIN_VALUE * personalCoeff;
	}

	//Agressivity Goes 
	public override void Update (){

		ManageLastShooter ();

		Decrease ();
		CheckShot ();
		CheckEnemiInSight ();
		CheckCurrentScore ();

		value *=  Mathf.Log (1 + ia.enemiesAround.Count + ia.creaturesAround.Count + ((lastShooter!=null)?1:0));
		value = Mathf.Max (this.MIN_VALUE, Mathf.Min (this.MAX_VALUE, value));

	}

	public void CheckShot(){
		if (lastShooter != ia.player.health.lastShooter || lastHit != ia.player.health.lastHitDate){
			lastHit = ia.player.health.lastHitDate;
			lastShooter = ia.player.health.lastShooter;
			value += personalCoeff * SHOT_VALUE;
		}
	}

	public void CheckEnemiInSight(){
		if (ia.closestEnemy != null){
			float dist = Vector3.Distance (ia.closestEnemy.gameObject.transform.position, ia.player.gameObject.transform.position);
			value += personalCoeff * (ia.maxAimingDistance / (dist + 1)) * ENEMY_IN_SIGHT_MULTIPLIER * Time.deltaTime;
			//Some analysis must be made to compare decrease and increase values
		}
	}

	public void CheckCurrentScore(){}

	public void ManageLastShooter(){
		timer += Time.deltaTime;
		if(timer >= FORGET_DELAY || ia.player.health.dead){
			timer = 0f;
			lastShooter = null;
			lastHit = 0f;
		}
	}
		
}

using UnityEngine;
using System.Collections;

public class IABehavior {

	public static float LAST_SHOOTER_FORGET_TIME = 5f;
	public static float LOOK_AT_SPEED = 100f;

	public IA ia;

	public bool lookAtLastShooter;
	public float lastShooterTimer;

	public IABehavior(IA ia){
		this.ia = ia;
		lastShooterTimer = 0f;
	}

	public virtual void Run(){
		LookAtTarget ();
	}

	public void LookAtTarget(){ // TODO : This need some fix : 

		if (ia.player.health.lastShooter != null){// &&  lookAtLastShooter && Time.time - ia.player.health.lastHitDate < LAST_SHOOTER_FORGET_TIME){
			Vector3 tmp = ia.player.health.lastShooter.transform.position - ia.gameObject.transform.position;
			Quaternion newRot = Quaternion.LookRotation (tmp);
			ia.gameObject.transform.rotation = Quaternion.Lerp (ia.gameObject.transform.rotation, newRot,LOOK_AT_SPEED);
		}
	}

	public virtual bool endCondition(){
		return true;
	}

	public virtual float EvaluatePriority(){
		return -1f;
	}

	public virtual void Setup(){
		
	}

	public virtual void Reset(){
		
	}
		
}

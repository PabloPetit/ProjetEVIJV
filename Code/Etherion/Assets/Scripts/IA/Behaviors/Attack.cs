using UnityEngine;
using System.Collections;

public class Attack : IABehavior {


	Aggressivity aggressivity;

	Player target;

	public StdIAWeapon weapon;

	public Attack(IA ia) : base(ia){
		aggressivity = (Aggressivity)ia.desires [typeof(Aggressivity)];
		weapon = ia.GetComponent<StdIAWeapon> ();
		weapon.barrel = ia.barrel;
	}


	public override void Run(){
		//
		if (target != null){
			if(target.health.dead){
				target = null;
			}else{
				AttackTarget ();	
			}
		}else{
			FindTarget ();
			ia.nav.speed = EnemyController.RUN_SPEED;
		}

	}

	public void FindTarget(){
		if (ia.closestEnemy != null){
			target = ia.closestEnemy;
			return;
		}
		if (ia.closestCreature != null){
			target = ia.closestCreature;
			return;
		}
	}

	public void AttackTarget(){

		AimTarget ();
		ia.nav.SetDestination (target.transform.position);

		Debug.Log (target);
		Debug.Log (weapon);

		if (ia.isTargetVisible (target.gameObject,weapon.range) && !weapon.overLoaded){
			ia.nav.speed = EnemyController.WALK_SPEED;
			if (IsTargetInSight ()){
				weapon.Shoot ();
			}
		}else{
			ia.nav.speed = EnemyController.RUN_SPEED;
		}


	}

	public bool IsTargetInSight(){
		return true;
	}

	public void AimTarget(){

		Vector3 tmp = target.transform.position - ia.gameObject.transform.position;
		Quaternion newRot = Quaternion.LookRotation (tmp);
		ia.gameObject.transform.rotation = Quaternion.Lerp (ia.gameObject.transform.rotation, newRot, 360f);
		ia.barrel.transform.rotation = Quaternion.Lerp (ia.barrel.transform.rotation, newRot, 360f);

	}

	public override float EvaluatePriority(){
		return aggressivity.value;
	}

	public override void Setup(){

	}

}

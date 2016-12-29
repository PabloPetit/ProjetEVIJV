using UnityEngine;
using System.Collections;

public class Attack : IABehavior {


	public static float MIN_ENEMY_DISTANCE = 50f;

	public static float AIM_SPEED = 20f/360f;
	public static float IN_SIGHT_ANGLE = 3f;

	public static float FORGET_TARGET = 4f;

	public float timer;


	Aggressivity aggressivity;
	Cowardice cowardice;

	Player target;

	public StdIAWeapon weapon;

	public Attack(IA ia) : base(ia){
		aggressivity = (Aggressivity)ia.desires [typeof(Aggressivity)];
		cowardice = (Cowardice)ia.desires [typeof(Cowardice)];

		weapon = ia.GetComponent<StdIAWeapon> ();
		weapon.barrel = ia.barrel;

		timer = 0f;
	}


	public override void Run(){
		base.Run ();

		ForgetTarget ();
		ia.nav.

		if (target != null){
			AttackTarget ();	
		}else{
			FindTarget ();
			ia.nav.speed = EnemyController.RUN_SPEED;
		}

	}

	public void ForgetTarget(){
		if (target != null){
			timer += Time.deltaTime;

			if (target.health.dead || Vector3.Distance (ia.gameObject.transform.position,target.transform.position)>ia.maxAimingDistance
				|| timer > FORGET_TARGET ){

				timer = 0f;
				target = null;
			}
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

		if (ia.isTargetVisible (target.gameObject,ia.maxAimingDistance) && !weapon.overLoaded){
			ia.nav.speed = EnemyController.WALK_SPEED;
			if (IsTargetInSight ()){
				weapon.Shoot ();
			}
		}else{
			ia.nav.speed = EnemyController.RUN_SPEED;
		}

		if (Vector3.Distance (ia.gameObject.transform.position, target.gameObject.transform.position) > MIN_ENEMY_DISTANCE / aggressivity.personalCoeff){
			ia.nav.SetDestination (target.transform.position);
		}else{
			ia.nav.speed = 0f; 
		}

	}

	public bool IsTargetInSight(){
		return Vector3.Angle (target.transform.position - ia.transform.position,ia.barrel.transform.forward) < IN_SIGHT_ANGLE;
	}

	public void AimTarget(){

		Vector3 tmp = target.transform.position - ia.gameObject.transform.position;
		Quaternion newRot = Quaternion.LookRotation (tmp);
		ia.barrel.transform.rotation = Quaternion.Lerp (ia.barrel.transform.rotation, newRot, AIM_SPEED);
		newRot.x = 0f;
		newRot.z = 0f;
		ia.gameObject.transform.rotation = Quaternion.Lerp (ia.gameObject.transform.rotation, newRot,AIM_SPEED);
	}

	public override float EvaluatePriority(){
		return Mathf.Max (0f,aggressivity.value - cowardice.value / 2 );
	}

	public override void Setup(){

	}

}

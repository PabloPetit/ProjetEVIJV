using UnityEngine;
using System.Collections;

public class GoToPosition : Behavior{


	public static int TYPE = 1;

	public static float MIN_TARGET_DISTANCE = 5f;

	Vector3 target;

	NavMeshAgent agent;

	PlayerWeapon weapon;

	GameObject head;

	public GoToPosition(int priority, Vector3 target ) : base(priority) {
		this.target = target;
		agent = GetComponent<NavMeshAgent> ();
		weapon = GetComponent<PlayerWeapon> ();
		agent.SetDestination (target);
	} 

	public virtual void Run(){

	}
		

	public override bool endCondition(){
		return false;//! (agent.remainingDistance() < MIN_TARGET_DISTANCE);
	}

}

using UnityEngine;
using System.Collections;

public class Damage {

	public static int playerMask = LayerMask.GetMask ("Player");
	public static int creatureMask = LayerMask.GetMask ("Creature");

	protected int side;


	protected GameObject shooter;

	protected bool hitMarker;

	protected float initialDamage;
	protected float minDamage;
	protected float damageDecrease;


	public static void DoDamage(GameObject shooter, GameObject target, float damage, bool friendFire, int side){
		if (IsCreatureLayer (target.layer)){

			CreatureHealth health = target.GetComponent<CreatureHealth> ();
			if (health != null){
				health.TakeDamage (damage, shooter);
			}
			
		}else if(IsPlayerLayer (target.layer)){
			PlayerState state = target.GetComponent<PlayerState> ();
			PlayerHealth health = target.GetComponent<PlayerHealth> ();
			if (health != null && state != null && (friendFire || (state.side != side))){
				health.TakeDamage (shooter, damage);
			}
		}
	}

	public static bool IsCreatureLayer (int layer){
		return ((1 << layer) & creatureMask) != 0;
	}

	public static bool IsPlayerLayer (int layer){
		return ((1 << layer) & playerMask) != 0;
	}

}

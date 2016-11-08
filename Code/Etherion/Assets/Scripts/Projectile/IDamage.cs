using UnityEngine;
using System.Collections;

public interface Damage {

	public static int playerMask = LayerMask.GetMask ("Player");


	protected GameObject shooter;
	protected bool hitMarker;

	protected float initialDamage;
	protected float minDamage;
	protected float damageDecrease;


	void DoDamage (float damage, int id);

	void ZoneDamage ();

	static bool IsCreatureLayer (int layer){
		return ((1 << layer) & creatureMask) != 0;
	}

	public static bool IsPlayerLayer (int layer){
		return ((1 << layer) & playerMask) != 0;
	}

}

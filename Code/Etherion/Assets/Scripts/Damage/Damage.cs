using UnityEngine;
using System.Collections;

public class Damage
{

	public static int playerMask = LayerMask.GetMask ("Player");


	public static void DoDamage (Player shooter, Player target, float damage, bool friendlyFire, bool hitMarker)
	{
		if (friendlyFire || shooter.side != target.side) {
			EventName targetDamage = new EventName (Player.DAMAGE_CHANNEL, target.id);
			EventManager.TriggerAction (targetDamage, new object[]{ damage, shooter.id });
			if (hitMarker) {
				EventName hit = new EventName (HitMarker.HITMARKER_CHANNEL);
				EventManager.TriggerAction (hit, new object[]{ });
			}
		}
	}

	public static void DoZoneDamage (Player shooter, Transform transform, float radius, IDamage damages)
	{
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, radius, playerMask);
		foreach (Collider col in hitColliders) {
			GameObject go = col.gameObject;
			float dist = Vector3.Distance (transform.position, col.gameObject.transform.position);
			float adjustedDamage = Mathf.Max (damages.initialDamage - (damages.damageDecrease * dist), damages.minDamage);
			Player target = go.GetComponent<Player> ();
			DoDamage (shooter, target, adjustedDamage, damages.friendlyFire, damages.hitMarker);
		}
	}

	public static bool IsPlayerLayer (int layer)
	{
		return ((1 << layer) & playerMask) != 0;
	}
}

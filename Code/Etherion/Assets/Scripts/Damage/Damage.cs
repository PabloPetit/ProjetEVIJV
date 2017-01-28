using UnityEngine;
using System.Collections;

public class Damage
{

	public static int playerMask = LayerMask.GetMask ("Player");


	public static void DoDamage (Player shooter, Player target, float damage, bool friendlyFire)
	{
		if (friendlyFire || shooter.side != target.side) {
			EventName targetDamage = new EventName (Player.DAMAGE_CHANNEL, target.id);
			EventManager.TriggerAction (targetDamage, new object[]{ damage, shooter });
		}
	}

	public static void DoZoneDamage (Player shooter, Vector3 position, float radius, IDamage damages)
	{
		Collider[] hitColliders = Physics.OverlapSphere (position, radius, playerMask);
		foreach (Collider col in hitColliders) {
			GameObject go = col.gameObject;
			float dist = Vector3.Distance (position, col.gameObject.transform.position);
			float adjustedDamage = Mathf.Max (damages.initialDamage - (damages.damageDecrease * dist), damages.minDamage);
			Player target = go.GetComponent<Player> ();
			DoDamage (shooter, target, adjustedDamage, damages.friendlyFire);
		}
	}

	public static bool IsPlayerLayer (int layer)
	{
		return ((1 << layer) & playerMask) != 0;
	}
}

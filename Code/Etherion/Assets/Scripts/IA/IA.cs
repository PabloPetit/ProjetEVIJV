using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour
{
	
	int playerMask;
	int environnementMask;


	public float visionAngle;
	public float visionRadius;
	public float maxAimingDistance;

	Player player;
	GameObject head;


	public float navDestinationRadius;
	NavMeshAgent nav;
	Vector3 navDestination;


	Ray shootRay;
	RaycastHit shootHit;

	void Start ()
	{
		player = GetComponent<Player> ();
		nav = GetComponent<NavMeshAgent> ();
		navDestination = Vector3.zero;
	}


	void SetNewRandomDestination ()
	{
		navDestination = Random.insideUnitSphere * navDestinationRadius;
		navDestination += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition (navDestination, out hit, navDestinationRadius, 1);
		navDestination = hit.position;
	}

	bool isTargetVisible (GameObject eye, GameObject rayTarget, float range)
	{
		bool res = false;
		shootRay.origin = eye.transform.position;
		shootRay.direction = (rayTarget.transform.position - eye.transform.position).normalized;

		if (Physics.Raycast (shootRay, out shootHit, maxAimingDistance, playerMask | environnementMask)) {
			if (shootHit.transform.gameObject == rayTarget) {
				res = true;
			}
		}
		return res;
	}

	Player getClosestPlayer (bool enemy, bool creature)
	{
		
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, visionRadius, playerMask);
		Player closest = null;
		float minDistance = visionRadius + 1f;

		foreach (Collider col in hitColliders) {
			
			if (Vector3.Angle (col.gameObject.transform.position - transform.position, transform.forward) < visionAngle) {
				float dist = Vector3.Distance (transform.position, col.gameObject.transform.position);

				if (dist < minDistance && isTargetVisible (head, col.gameObject, visionRadius)) {

					Player player = col.gameObject.GetComponent<Player> ();

					if (player != null && player.isCreature == creature && ((enemy && this.player.side != player.side) || ((!enemy && this.player.side == player.side)))) {
						closest = player;
						minDistance = dist;	
					}
				}
			}
		}
		return closest;
	}


}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IA : MonoBehaviour
{

	/*
	 * 
	 *    Constants
	 * 
	 */ 


	// Layers

	public int playerMask;
	public int environnementMask;

	// Player Parts

	public Player player;
	public GameObject head;
	public GameObject barrel;

	public NavMeshAgent nav;

	// Ray Shooting

	Ray shootRay;
	RaycastHit shootHit;


	// Public Constants

	public float visionAngle = 60f;
	public float visionRadius = 250f;
	public float maxAimingDistance = 1000f;

	public float navDestinationRadius = 100f;


	// Variables 

	public GameManager gameManager;

	public Dictionary<System.Type, Desire> desires;

	public List<IABehavior> behaviors;

	public IABehavior currentBehavior;

	// Visibility is not checked for Allies : 

	public Player closestFriend; // Not Checked
	public Player closestEnemy; // Checked
	public Player closestCreature; // Checked

	public List<Player> playersAround; // Not Checked

	public List<Player> friendsAround; // Not Checked 
	public List<Player> enemiesAround; // Checked
	public List<Player> creaturesAround; // Checked


	protected void Start ()
	{
		playerMask = LayerMask.GetMask ("Player");
		environnementMask = LayerMask.GetMask ("Environement");

		player = GetComponent<Player> ();
		nav = GetComponent<NavMeshAgent> ();

		playersAround = new List<Player>();
		friendsAround = new List<Player>();
		enemiesAround = new List<Player>();
		creaturesAround = new List<Player>();

		behaviors = new List<IABehavior> ();
		desires = new Dictionary<System.Type, Desire> ();

		gameManager = FindObjectOfType<GameManager> ();

		SetHeadAndBarrel ();
		SetDesires ();
		SetBehaviors ();
	}

	public virtual void SetHeadAndBarrel(){
		
	}

	public virtual void SetDesires(){

	}

	public virtual void SetBehaviors(){

	}


	// This update will collect Data and Update Desires
	public void Update(){ //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@  FixedUpadate ???? @@@@@@@@@@@@@@@@@@@@@@@@@@@@

		if(player.health.dead){
			return;
		}

		ClearPlayersAround ();
		GetClosestPlayers ();
		UpdateDesires ();
		SelectBehavior ();
		currentBehavior.Run ();
	}


	public void SelectBehavior(){

		IABehavior tmp = null;
		float maxPriority = -2f;


		foreach (IABehavior b in behaviors){
			if(b == currentBehavior){
				continue;
			}
			float prioTmp = b.EvaluatePriority ();
			if (prioTmp >= maxPriority){
				tmp = b;
				maxPriority = prioTmp;
			}
		}

		if (currentBehavior != null && currentBehavior.EvaluatePriority () < maxPriority){ 
			currentBehavior.Reset ();
			currentBehavior = tmp;
			currentBehavior.Setup ();
		}

		if (currentBehavior == null){
			currentBehavior = tmp;
			currentBehavior.Setup ();
		}


	}

	public void UpdateDesires(){
		foreach(Desire d in desires.Values){
			d.Update ();
		}
	}
		


	/*
	 * 
	 *     USEFULL
	 * 
	 */



	public void SetNewRandomDestination ()
	{
		Vector3 navDestination = Vector3.zero;
		navDestination = Random.insideUnitSphere * navDestinationRadius;
		navDestination += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition (navDestination, out hit, navDestinationRadius, 1);
		nav.SetDestination (hit.position);
	}

	public bool isTargetVisible (GameObject rayTarget, float range)
	{
		bool res = false;
		shootRay.origin = head.transform.position;
		shootRay.direction = (rayTarget.transform.position - head.transform.position).normalized;

		if (Physics.Raycast (shootRay, out shootHit, maxAimingDistance, playerMask | environnementMask)) {
			if (shootHit.transform.gameObject == rayTarget) {
				res = true;
			}
		}
		return res;
	}

	public void GetClosestPlayers ()
	{

		Collider[] hitColliders = Physics.OverlapSphere (transform.position, visionRadius, playerMask);

		float minDistEnemy = visionRadius + 1f;
		float minDistFriend = visionRadius + 1f;
		float minDistCreature = visionRadius + 1f;

		foreach (Collider col in hitColliders) {

			float dist = Vector3.Distance (transform.position, col.gameObject.transform.position);

			Player player = col.gameObject.GetComponent<Player> ();

			if (player == null) {
				continue;
			} 

			if (!playersAround.Contains (player)){
				playersAround.Add (player);
			}
				
			if (player.side == this.player.side) {
				if (player.id != this.player.id) {

					if (!friendsAround.Contains (player)) {
						friendsAround.Add (player);
					}

					if (dist < minDistFriend) {
						minDistFriend = dist;
						closestFriend = player;
					}
				}
			} else {
				//Now checking Angle and Visibility
				if (Vector3.Angle (col.gameObject.transform.position - transform.position, transform.forward) < visionAngle && isTargetVisible (col.gameObject, visionRadius)) {
				
					if (player.isCreature){

						if (!creaturesAround.Contains (player)) {
							creaturesAround.Add (player);
						}

						if (dist < minDistCreature) {
							minDistCreature = dist;
							closestCreature = player;
						}
						
					}else{

						if (!enemiesAround.Contains (player)) {
							enemiesAround.Add (player);
						}

						if (dist < minDistEnemy) {
							minDistEnemy = dist;
							closestCreature = player;
						}
						
					}
				}
			}
		}
	}

	public float ForcesRatio(){
		if (enemiesAround.Count == 0 || friendsAround.Count == 0)
			return 1f;
		else
			return (float)friendsAround.Count / (float)enemiesAround.Count;
	}

	public void ClearPlayersAround(){
		closestFriend = null;
		closestEnemy = null;
		closestCreature = null;

		playersAround.Clear ();
		friendsAround.Clear ();
		enemiesAround.Clear ();
		creaturesAround.Clear ();
	}
		

}

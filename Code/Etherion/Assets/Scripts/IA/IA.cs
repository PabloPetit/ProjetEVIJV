﻿using UnityEngine;
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

	public float visionAngle;
	public float visionRadius;
	public float maxAimingDistance;

	public float navDestinationRadius;


	// Variables 

	public GameManager gameManager;

	public Dictionary<string, Desire> desires;

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
		desires = new Dictionary<string, Desire> ();

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
		ClearPlayersAround ();
		GetClosestPlayers ();
		UpdateDesires ();
		SelectBehavior ();
		currentBehavior.Run ();
	}


	public void SelectBehavior(){
		if(currentBehavior != null && currentBehavior.endCondition ()){
			currentBehavior.Reset ();
			currentBehavior = null;
		}

		IABehavior tmp = null;
		float maxPriority = 0f;

		foreach (IABehavior b in behaviors){
			float prioTmp = b.EvaluatePriority ();
			if (prioTmp > maxPriority){
				tmp = b;
				maxPriority = prioTmp;
			}
		}

		if (currentBehavior != null && currentBehavior.EvaluatePriority () < maxPriority){ 
			currentBehavior.Reset ();
			currentBehavior = tmp;
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

			playersAround.Add (player);

			if (player.side == this.player.side) {
				friendsAround.Add (player);

				if (dist < minDistFriend) {
					minDistFriend = dist;
					closestFriend = player;
				}
			} else {
				//Now checking Angle and Visibility
				if (Vector3.Angle (col.gameObject.transform.position - transform.position, transform.forward) < visionAngle && isTargetVisible (col.gameObject, visionRadius)) {
				
					if (player.isCreature){

						creaturesAround.Add (player);

						if (dist < minDistCreature) {
							minDistCreature = dist;
							closestCreature = player;
						}
						
					}else{

						enemiesAround.Add (player);

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

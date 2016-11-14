using UnityEngine;
using System.Collections;

public class MonsterController : MonoBehaviour {

	public const int STATE_WANDER = 1;
	public const int STATE_GO_TO_TARGET = 2;
	public const int STATE_ATTACK_TARGET = 3;
	public const int STATE_GO_TO_BASE = 4;

	public const float WANDER_RADIUS = 100f;
	public const float DEFENSE_RADIUS = 300f;
	public const float PURSUIT_RADIUS = 500f;

	public const float DETECTION_ANGLE = 45f;
	public const float DETECTION_DISTANCE = DEFENSE_RADIUS;

	public const float WANDER_SPEED = 10f;
	public const float ATTACK_SPEED = 25f;

	public const float ATTACK_DELAY = 3f;
	public const float ATTACK_RANGE = 2f;



	public float damage;



	int playerMask;
	int environnementMask;

	// MonsterBase base;

	private Vector3 initialPosition;
	public int currentState;

	private GameObject target;
	private Player targetPlayer;

	private NavMeshAgent nav;
	private Player player;

	private float timer;
	Ray shootRay;
	RaycastHit shootHit;
	Vector3 navDestination;

	// Use this for initialization
	void Start () {
		playerMask = LayerMask.GetMask ("Player");
		environnementMask = LayerMask.GetMask ("Environement");
		initialPosition = transform.position;
		nav = GetComponent<NavMeshAgent> ();
		player = GetComponent<Player> ();
		timer = 0f;

		GoWander ();
	}

	void FixedUpdate () {
		timer += Time.fixedDeltaTime;
		if (!player.health.dead){
			switch(currentState){
			case STATE_WANDER: 
				Wander ();
				break;
			case STATE_GO_TO_TARGET:
				GoToTarget ();
				break;
			case STATE_GO_TO_BASE:
				break;
			case STATE_ATTACK_TARGET:
				Attack ();
				break;
				
			}
		}
	}



	/*
	 * 
	 *    WANDER 
	 * 
	 */

	void GoWander(){
		currentState = STATE_WANDER;
		nav.speed = WANDER_SPEED;
		timer = 0f;
		target = null;
		targetPlayer = null;
		SetNewWanderDestination ();
	}

	void SetNewWanderDestination(){
		navDestination = Random.insideUnitSphere * WANDER_RADIUS;
		navDestination += initialPosition;
		NavMeshHit hit;
		NavMesh.SamplePosition (navDestination, out hit, WANDER_RADIUS, 1);
		navDestination = hit.position;
		nav.SetDestination (navDestination);
	}

	void Wander(){

		setTarget ();

		if (target != null){
			Go_GoToTarget ();
		}
			
		else if((nav.remainingDistance < 5f && timer > 3f) || timer > 20f) {
			SetNewWanderDestination ();
		}
	}

	void setTarget (){
		Collider[] hitColliders = Physics.OverlapSphere (transform.position, DETECTION_DISTANCE, playerMask);
		GameObject closest = null;
		float minDistance = DETECTION_DISTANCE + 1f;
		foreach (Collider col in hitColliders) {
			if (Vector3.Angle (col.gameObject.transform.position - transform.position, transform.forward) < DETECTION_ANGLE) {
				float dist = Vector3.Distance (transform.position, col.gameObject.transform.position);
				if (dist < minDistance && isTargetVisible (col.gameObject, DETECTION_DISTANCE)) {

					targetPlayer = col.gameObject.GetComponent<Player> ();

					if (targetPlayer == null){
						targetPlayer = col.gameObject.GetComponentInChildren<Player> ();
					}
					if (targetPlayer == null || targetPlayer.side == player.side){
						continue;
					}

					closest = col.gameObject;
					minDistance = dist;

				}
			}
		}
		if (closest != null) {
			target = closest;
		}
	}


	/*
	 * 
	 *  GO_TO_TARGET
	 * 
	 * 
	 */ 


	void Go_GoToTarget(){
		timer = 0f;
		currentState = STATE_GO_TO_TARGET;
		nav.speed = ATTACK_SPEED;
		nav.SetDestination (target.transform.position);
	}

	void GoToTarget(){
			
		if (target == null || targetPlayer.health.dead){
			//Et pourquoi pas aussi si le joueur est loin de la base
			GoToBase ();
		}

		else if (Vector3.Distance (target.transform.position,transform.position) < ATTACK_RANGE){
			GoAttack ();
		}
			
		else if (timer > .1f){
			nav.SetDestination (target.transform.position);
			timer = 0f;
		}
	}

	/*
	 * 
	 *  ATTACK
	 * 
	 * 
	 */ 

	void GoAttack(){
		currentState = STATE_ATTACK_TARGET;
		timer = 0f;
		DoDamage ();
		nav.Stop ();
	}


	void Attack(){

		if (target == null ){
			nav.Resume ();
			GoWander ();
		}

		else if (timer > ATTACK_DELAY){
			nav.Resume ();
			Go_GoToTarget ();
		}
	}

	void DoDamage(){
		EventName targetHealth = new EventName (Player.DAMAGE_CHANNEL, targetPlayer.id);
		EventManager.TriggerAction (targetHealth, new object[]{damage,player.id});
	}


	/*
	 * 
	 * 
	 * GO TO BASE
	 * 
	 */


	void Go_GoToBase(){
		timer = 0f;
		currentState = STATE_GO_TO_BASE;
		nav.SetDestination (initialPosition);
	}

	void GoToBase(){
		if (nav.remainingDistance < 10f){
			GoWander ();
		}
	}

	/*
	 * 
	 * 
	 * 
	 * 
	 */ 

	bool isTargetVisible (GameObject rayTarget, float range)
	{
		bool res = false;
		shootRay.origin = transform.position+Vector3.up;

		shootRay.direction = (rayTarget.transform.position - shootRay.origin).normalized;

		if (Physics.Raycast (shootRay, out shootHit, DETECTION_DISTANCE, playerMask | environnementMask)) {
			if (shootHit.transform.gameObject == rayTarget) {
				res = true;
			}
		}
		return res;
	}

}

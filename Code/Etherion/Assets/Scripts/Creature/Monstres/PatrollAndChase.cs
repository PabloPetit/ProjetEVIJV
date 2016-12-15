using UnityEngine;
using System.Collections;

public class PatrollAndChase : MonoBehaviour
{
	public float enemySpeed = 30.0f;
	private Transform myTarget;
	private GameObject go;
	private Vector3 startPosition;
	private bool isWandering = true;
	private bool inCombat = false;
	public float enemyWanderSpeed = 10.0f;
	public float wanderRange = 100.0f;
	private float wanderDelayTimer;
	public float distanceToCombat = 200.0f;
	public float distanceToFight = 20.0f;
	public float distanceToDropCombat = 500.0f;
	private bool chasingPlayer = false;
	private NavMeshAgent agent;
	private bool isReturning = false;

	MonsterHealth health;
	Animator anim;
	AudioSource audio;


	void Awake ()
	{
		go = GameObject.FindGameObjectWithTag ("Player");
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = enemyWanderSpeed;
		startPosition = this.transform.position;
		anim = transform.GetComponent<Animator> ();
		anim.SetTrigger ("Walk");
	}

	void Update ()
	{
		//Debug.Log(transform.position);

		startWander ();

		float distanceFromPlayer = Vector3.Distance (transform.position, go.transform.position);
		float distanceChasedCombat = Vector3.Distance (transform.position, startPosition);
		//Debug.Log(distanceFromPlayer);
		//Debug.Log(isReturning);
		if ((distanceFromPlayer <= distanceToCombat) && !isReturning) {
			chasingPlayer = true;
			//Debug.Log("chasing true");
		}
		//if it chased the player too far run back and start wandering around, by setting the wander delay timer to 0 so it runs back straight away.

		if (chasingPlayer) {
			//Debug.Log ("chasing");

			myTarget = go.transform;
			while ((distanceFromPlayer > distanceToFight) && !(distanceChasedCombat >= distanceToDropCombat))
				agent.SetDestination (myTarget.transform.position);
			agent.speed = enemySpeed;
			if (distanceFromPlayer > distanceToFight) {
				anim.SetTrigger ("Fight");
			}


		}
		if ((distanceChasedCombat >= distanceToDropCombat) && chasingPlayer) {
			chasingPlayer = false;
			//Debug.Log("too far");
			wanderDelayTimer = 0.0f;
			isReturning = true;

		}


	}

	void startWander ()
	{

		wanderDelayTimer -= Time.deltaTime;
		//Debug.Log(wanderDelayTimer);
		if (wanderDelayTimer <= 0 && !chasingPlayer) {
			Debug.Log (chasingPlayer);
			Wander ();
			wanderDelayTimer = Random.Range (2.0f, 4.0f);
			//Debug.Log(wanderDelayTimer);
		}
	}

	void Wander ()
	{
		/*agent.speed = enemyWanderSpeed;
		Vector3 navDestination = Random.insideUnitSphere * wanderRange;
		navDestination += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition (navDestination,out hit, wanderRange,1);
		navDestination = hit.position;*/

		agent.speed = enemyWanderSpeed;
		Vector3 destination = startPosition + new Vector3 (Random.Range (-wanderRange, wanderRange), 0 /*transform.position.y*/, Random.Range (-wanderRange, wanderRange));
		newDestination (destination);

		//agent.SetDestination (navDestination);
	}

	public void newDestination (Vector3 targetPoint)
	{
		//Debug.Log("new destination");

		agent.SetDestination (targetPoint);

		//Debug.Log(targetPoint);
	}
		


}
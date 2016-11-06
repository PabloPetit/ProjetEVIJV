using UnityEngine;
using System.Collections;

public class PatrollAndChase : MonoBehaviour 
{
	public float enemySpeed = 5.0f;
	private Transform myTarget;
	private GameObject go;
	private Vector3 startPosition;
	private bool isWandering = true;
	private bool inCombat = false;
	public float enemyWanderSpeed = 30.0f;
	public float wanderRange = 100.0f;
	private float wanderDelayTimer;
	public float distanceToCombat = 100.0f;
	public float distanceToDropCombat = 200.0f;
	private bool chasingPlayer = false;
	private NavMeshAgent agent;
	private bool isReturning = false;


	void Awake()
	{
		go = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<NavMeshAgent>();
		agent.speed  = enemyWanderSpeed;
		startPosition = this.transform.position;
	}

	void Update () 
	{
		//Debug.Log(transform.position);

		startWander();
		/*float distanceFromPlayer = Vector3.Distance(transform.position, go.transform.position);
		float distanceChasedCombat = Vector3.Distance(transform.position, startPosition);
		//Debug.Log(distanceFromPlayer);
		if(distanceFromPlayer <= distanceToCombat && !isReturning)
		{
			chasingPlayer = true;
		}
		//if it chased the player too far run back and start wandering around, by setting the wander delay timer to 0 so it runs back straight away.
		if(distanceChasedCombat >= distanceToDropCombat)
		{
			chasingPlayer = false;
			wanderDelayTimer = 0.0f;
		}
		if(chasingPlayer)
		{
			myTarget = go.transform;
			agent.SetDestination(myTarget.transform.position);
			agent.speed = enemySpeed;
		}*/

	}

	void startWander()
	{

		wanderDelayTimer -=Time.deltaTime;
		Debug.Log(wanderDelayTimer);
		if(wanderDelayTimer <= 0 && !chasingPlayer)
		{
			Wander();
			wanderDelayTimer = Random.Range(2.0f, 4.0f);
			//Debug.Log(wanderDelayTimer);
		}
	}

	void Wander()
	{
		/*agent.speed = enemyWanderSpeed;
		Vector3 navDestination = Random.insideUnitSphere * wanderRange;
		navDestination += transform.position;
		NavMeshHit hit;
		NavMesh.SamplePosition (navDestination,out hit, wanderRange,1);
		navDestination = hit.position;*/

		agent.speed = enemyWanderSpeed;
		Vector3 destination = startPosition + new Vector3 (Random.Range (-wanderRange, wanderRange),0 /*transform.position.y*/, Random.Range(-wanderRange, wanderRange));
		newDestination(destination);

		//agent.SetDestination (navDestination);
	}

	public void newDestination(Vector3 targetPoint)
	{
		agent.SetDestination (targetPoint);
		Debug.Log("new destination");
		Debug.Log(targetPoint);
	}


}
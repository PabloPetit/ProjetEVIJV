﻿using UnityEngine;
using System.Collections;

public class PatrollAndChase2 : MonoBehaviour 
{
	public float enemySpeed = 5.0f;
	public Transform myTarget;
	public GameObject go;
	private Vector3 startPosition;
	private bool isWandering = true;
	public bool inCombat = false;
	public float enemyWanderSpeed = 3.0f;
	public float wanderRange = 100.0f;
	public float wanderDelayTimer;
	public float distanceToCombat = 200.0f;
	public float distanceToDropCombat = 100000.0f;
	public bool chasingPlayer = false;
	public UnityEngine.AI.NavMeshAgent agent;
	public bool isReturning = false;


	void Awake()
	{
		go = GameObject.FindGameObjectWithTag("Player");
		agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
		agent.speed  = enemyWanderSpeed;
		startPosition = this.transform.position;
	}
	void Update () 
	{
		Debug.Log(transform.position);

		startWander();
		float distanceFromPlayer = Vector3.Distance(transform.position, go.transform.position);
		float distanceChasedCombat = Vector3.Distance(transform.position, startPosition);

		if(distanceFromPlayer <= distanceToCombat && !isReturning)
		{
			chasingPlayer = true;
		}
		//if it chased the player too far run back and start wandering around, by setting the wander delay timer to 0 so it runs back straight away.
		if(distanceChasedCombat >= distanceToDropCombat)
		{
			Debug.Log (" drop combat");
			Debug.Log(distanceChasedCombat);
			Debug.Log(distanceToDropCombat);
			chasingPlayer = false;
			wanderDelayTimer = 0.0f;
		}
		if(chasingPlayer)
		{
			myTarget = go.transform;
			agent.SetDestination(myTarget.transform.position);
			agent.speed = enemySpeed;
			Debug.Log("Chasing");
		}

	}
	void startWander()
	{

		wanderDelayTimer -=Time.deltaTime;
		if(wanderDelayTimer <= 0 && !chasingPlayer)
		{
			Wander();
			wanderDelayTimer = 5.0f;
		}
	}
	void Wander()
	{
		agent.speed = enemyWanderSpeed;
		Vector3 destination = startPosition + new Vector3 (Random.Range (-wanderRange, wanderRange),0 /*transform.position.y*/, Random.Range(-wanderRange, wanderRange));
		newDestination(destination);
	}
	public void newDestination(Vector3 targetPoint)
	{
		agent.SetDestination (targetPoint);
	}
}
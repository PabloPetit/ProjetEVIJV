using UnityEngine;
using System.Collections;

public class PatrollingEnemy : MonoBehaviour {

	public Vector3 startPosition;  //Give it a startPosition so it knows where it's 'home' location is.
	public bool wandering = true;  //Set a bool or state so it knows if it's wandering or chasing a player
	public bool chasing = true;
	public float wanderSpeed = 0.5f;  //Give it the movement speeds
	public GameObject target;  //The target you want it to chase
	public float wanderRange = 10.0f;
	public float velocityanim = 1;
	public float dampTime = 3;
	public NavMeshAgent agent;
	Animator anim;   
	//AudioSource enemyAudio;
	int walk = Animator.StringToHash("monster1Walk");
	GameObject go = GameObject.FindGameObjectWithTag("Player");


	//int maxDistance = 1;    


	//When the enemy is spawned via script or if it's pre-placed in the world we want it to first
	//Get it's location and store it so it knows where it's 'home' is
	//We also want ti set it's speed and then start wandering
	void Awake(){
		//Get the NavMeshAgent so we can send it directions and set start position to the initial location
		anim = GetComponent <Animator> ();
		//enemyAudio = GetComponent <AudioSource> ();
		agent = GetComponent("NavMeshAgent") as NavMeshAgent;
		agent.speed = wanderSpeed;  
		startPosition = this.transform.position;
		//Start Wandering
		InvokeRepeating("Wander", 1f, 5f);
		anim.SetTrigger (walk);
		//Transform targetPlayer = go.transform;
	}

	//When we wander we essentially want to pick a random point and then send the agent there
	//Random.Range is perfect for this.
	//If you're working on a hilly terrain you may want to change your y to a higher point and then
	//Use a raycast down to hit the 'terrain' point, rather than keeping y at 0.
	//y at 0 would only work if you have a completely flat floor.
	void Wander(){
		//Pick a random location within wander-range of the start position and send the agent there
		Vector3 destination = startPosition + new Vector3(Random.Range (-wanderRange, wanderRange), 
			0, 
			Random.Range (-wanderRange, wanderRange));
		NewDestination(destination);
		//velocityanim = 1;
		//anim.SetFloat("speed",velocityanim);
		anim.SetTrigger (walk);

		// Change the audio clip of the audio source to the death clip and play it (this will stop the hurt clip playing).
		//enemyAudio.clip = ;
		//enemyAudio.Play ();
	}


	//Creating this as it's own method so we can send it directions other when it's just wandering.
	public void NewDestination(Vector3 targetPoint){
		//Sets the agents new target destination to the position passed in
		agent.SetDestination (targetPoint);
		anim.SetTrigger (walk);
	}
}
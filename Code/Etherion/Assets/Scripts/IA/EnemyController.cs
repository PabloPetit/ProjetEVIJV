using UnityEngine;
using System.Collections;

public class EnemyController : IA {



	Behavior currentBehavior;

	Player player;

	public Artefact artefact;


	void Start () {
		base.Start ();
		artefact = player.team.teamSlot.artefact;
	}
	
	// Update is called once per frame
	void Update () {

		MajDesires ();

		EvaluateBehavior ();

		currentBehavior.Run ();

	}



	void EvaluateBehavior(){

		if(currentBehavior.endCondition ()){
			currentBehavior = null;
		}

		if (currentBehavior == null){
			//SetBehavior (new GoToTarget(Behavior.EvaluatePriority(truc,truc,truc),artefact.gameObject));
		}
			
		SetBehavior (new GoToPosition(0,Vector3.zero));



	}

	void MajDesires(){
		//Look Around
		//Listen Communications
	}


	void SetBehavior(Behavior newBehavior){
		if (currentBehavior == null || newBehavior.GetPriority() > currentBehavior.GetPriority()){
			currentBehavior = newBehavior;
		}
	}
}

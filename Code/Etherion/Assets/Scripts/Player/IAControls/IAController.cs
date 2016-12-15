using UnityEngine;
using System.Collections;

public class IAController : MonoBehaviour {


	public static float VIEW_RANGE = 1500;
	public static float VIEW_ANGLE = 1500;

	Behavior currentBehavior;
	Desires desires;
	Player player;

	Artefact artefact;

	// Use this for initialization
	void Start () {
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

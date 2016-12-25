using UnityEngine;
using System.Collections;

public class IABehavior : MonoBehaviour {


	public string name;
	public IA ia;


	public IABehavior(IA ia){
		this.ia = ia;
		this.name = "STANDARD_BEHAVIOR";
	}

	public virtual void Run(){
		
	}

	public virtual bool endCondition(){
		return true;
	}

	public float EvaluatePriority(){
		return -1f;
	}

	public virtual void Setup(){
		
	}

	public virtual void Reset(){
		
	}
		
}

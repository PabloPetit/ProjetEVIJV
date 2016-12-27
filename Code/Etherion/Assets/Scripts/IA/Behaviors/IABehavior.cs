using UnityEngine;
using System.Collections;

public class IABehavior {


	public IA ia;

	public IABehavior(IA ia){
		this.ia = ia;
	}

	public virtual void Run(){
		
	}

	public virtual bool endCondition(){
		return true;
	}

	public virtual float EvaluatePriority(){
		return -1f;
	}

	public virtual void Setup(){
		
	}

	public virtual void Reset(){
		
	}
		
}

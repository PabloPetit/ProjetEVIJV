using UnityEngine;
using System.Collections;

public class Behavior : MonoBehaviour {


	public static int TYPE = -1;


	protected int priority;

	public Behavior(int priority){
		this.priority = priority;
	}

	public virtual void Run(){
		
	}

	public virtual bool endCondition(){
		return false;
	}

	public static int EvaluatePriority(int priorityType, bool leader){
		return 1;
	}


	public int GetPriority(){
		return priority;
	}

	public void SetPriority(int priority){
		this.priority = priority;
	}
}

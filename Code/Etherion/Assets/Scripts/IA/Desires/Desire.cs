using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;


public class Desire  {

	public float MAX_VALUE = 150f;
	public float MIN_VALUE = 1f;


	public float MAX_RAND = 2f;
	public float MIN_RAND  = .5f;

	public float DECREASE_TIME = 1f; // Time in seconds needed to reach 0 with decrease

	public IA ia;

	public float normalValue;
	public float personalCoeff;
	public float value;

	public Desire(IA ia){
		this.ia = ia;
		SetRandomCoeff ();
		Setup ();
		this.value = MIN_VALUE * personalCoeff;

	}
		

	public virtual void Setup(){

	}

	public virtual void Update(){
		
	}

	public void SetRandomCoeff(){
		personalCoeff = MIN_RAND + UnityEngine.Random.Range (0f, MAX_RAND - MIN_RAND);
	}


}
	
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;


public class Desire  {

	public string NAME = "DEFAULT_DESIRE_NAME";

	public float MAX_VALUE = 100f;
	public float MIN_VALUE = 1f;


	public float MAX_RAND = 5f;
	public float MIN_RAND  = .2f;

	public float DECREASE_TIME = 5f; // Time in seconds needed to reach 0 with decrease


	public IA ia;
	public Player player;


	public float normalValue;
	public float personalCoeff;
	public float value;

	public Desire(IA ia, Player player){
		this.ia = ia;
		this.player = player;
		Setup ();
		SetRandomCoeff ();
		this.value = MIN_VALUE * personalCoeff;
	}
		

	public virtual void Setup(){

	}

	public virtual void Update(){
		
	}

	public virtual void Decrease(){
		float diff = DECREASE_TIME * Time.deltaTime * ( MAX_RAND - personalCoeff ) ;
	    value = Mathf.Max (MIN_VALUE, value - diff);
	}

	public void SetRandomCoeff(){
		personalCoeff = MIN_VALUE * UnityEngine.Random.Range (0f, MAX_VALUE - MIN_VALUE);
	}


}
	
using UnityEngine;
using System.Collections;

public class Cowardice : Desire {


	public static float STD_MIN_VALUE = 25f;

	public static float CARRYING_ARTEFACT = 50f;

	public static float LOW_LIFE_MULTIPLIER = 1f;


	public Cowardice(IA ia, Player player) : base(ia, player){

	}


	public override void Setup (){
		this.MIN_VALUE = STD_MIN_VALUE * personalCoeff;
	}

	public override void Update (){
		Decrease ();
		CheckLife ();
		CheckCarriyingArtefact ();
		value = Mathf.Max (MIN_VALUE, Mathf.Min (MAX_VALUE, value));
	}

	public void CheckLife(){
		value += personalCoeff * (player.health.maxLife - player.health.life) * LOW_LIFE_MULTIPLIER * Time.deltaTime;
	}

	public void CheckCarriyingArtefact(){
		this.MIN_VALUE = STD_MIN_VALUE * personalCoeff;
	}

}

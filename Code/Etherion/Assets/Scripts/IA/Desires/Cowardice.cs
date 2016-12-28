using UnityEngine;
using System.Collections;

public class Cowardice : Desire {

	public static string NAME = "COWARDICE";


	public static float STD_MIN_VALUE = 5f;

	public static float CARRYING_ARTEFACT = 50f;

	public static float LOW_LIFE_MULTIPLIER = 1f;


	public Cowardice(IA ia) : base(ia){

	}


	public override void Setup (){
		this.MIN_VALUE = STD_MIN_VALUE * personalCoeff;
		this.MAX_VALUE /= 2;
	}

	public override void Update (){
		Decrease ();
		CheckLife ();
		value = Mathf.Max (MIN_VALUE + ((CheckCarriyingArtefact ())?CARRYING_ARTEFACT:0), Mathf.Min (MAX_VALUE, value));
	}

	public void CheckLife(){
		value += personalCoeff * (ia.player.health.maxLife - ia.player.health.life) * LOW_LIFE_MULTIPLIER * Time.deltaTime;
	}

	public bool CheckCarriyingArtefact(){
		bool carrying = false;
		foreach(Artefact art in ia.gameManager.artefacts){
			if(art.transporter == ia.player){
				carrying = true;
				break;
			}
		}
		return carrying;
	}

	public void Decrease(){
		float diff = Time.deltaTime * ( MAX_RAND - personalCoeff ) / DECREASE_TIME;
		value = Mathf.Max (MIN_VALUE, value - diff);
	}

}

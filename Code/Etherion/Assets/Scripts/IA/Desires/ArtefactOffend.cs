using UnityEngine;
using System.Collections;

public class ArtefactOffend : Desire {

	public float STD_VALUE = 50f;

	public ArtefactOffend(IA ia, Player player) : base(ia, player){}


	public override void Update ()
	{
		value = personalCoeff * STD_VALUE;
		value = Mathf.Max (MIN_VALUE, Mathf.Min (MAX_VALUE, value));
	}

	public void CheckScores(){
		// If score is bad, increase value
	}

}

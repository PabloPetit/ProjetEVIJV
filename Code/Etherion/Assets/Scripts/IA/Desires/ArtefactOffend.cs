using UnityEngine;
using System.Collections;

public class ArtefactOffend : Desire {

	public static string NAME = "ARTEFACT_OFFEND";

	public float STD_VALUE = 50f;

	public ArtefactOffend(IA ia) : base(ia){
		MIN_VALUE = STD_VALUE * personalCoeff;
	}


	public override void Update ()
	{
		value = personalCoeff * STD_VALUE;
		value = Mathf.Max (MIN_VALUE, Mathf.Min (MAX_VALUE, value));
	}

	public void CheckScores(){
		// If score is bad, increase value
	}

}

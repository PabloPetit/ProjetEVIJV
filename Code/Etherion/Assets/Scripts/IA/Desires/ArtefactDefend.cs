using UnityEngine;
using System.Collections;

public class ArtefactDefend : Desire {

	public static string NAME = "ARTEFACT_DEFEND";


	public float STD_VALUE = 50f;

	public ArtefactDefend(IA ia) : base(ia){}


	public override void Update ()
	{
		value = personalCoeff * STD_VALUE;
		value = Mathf.Max (MIN_VALUE, Mathf.Min (MAX_VALUE, value));
	}

	public void CheckScores(){
		// If score is bad, increase value
	}

}

using UnityEngine;
using System.Collections;

public class DefendTeamArtefact : IABehavior
{
	public static float MAX_VALUE = 130f;

	public static float ARTEFACT_TAKEN_VALUE = 1f;
	public static float DISTANCE_MULTIPLIER = 0.025f;


	ArtefactDefend artDefend;

	Artefact art;
	GameObject receptor;
	// Here we assume that the artefact and the receptor are close.


	public DefendTeamArtefact (IA ia) : base (ia)
	{
		artDefend = (ArtefactDefend)ia.desires [typeof(ArtefactDefend)];
		art = ia.player.team.teamSlot.artefact;
		receptor = ia.player.team.teamSlot.receptor;
	}

	public override void Run ()
	{
		base.Run ();
		ia.SetNavTarget (art.gameObject.transform.position);
	}

	public override float EvaluatePriority ()
	{
		float distanceToBaseValue = Vector3.Distance (art.gameObject.transform.position, receptor.transform.position);
		float takenValue = 0f;//((art.transporter != null) ? ARTEFACT_TAKEN_VALUE : 0) * artDefend.value;

		return  Mathf.Min (takenValue + distanceToBaseValue, MAX_VALUE);
	}

	public override void Setup ()
	{
		ia.nav.speed = EnemyController.RUN_SPEED;
	}


}

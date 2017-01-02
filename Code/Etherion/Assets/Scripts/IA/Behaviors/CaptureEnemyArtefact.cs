using UnityEngine;
using System.Collections;

public class CaptureEnemyArtefact : IABehavior
{


	ArtefactOffend artOffend;

	public static float CARRRYING_ARTEFACT_VALUE = 75f;
	public static float NO_AVAILABLE_ARTEFACT_VALUE = 300f;

	bool searching;
	bool noArtefactAvailable;
	Artefact target;


	public CaptureEnemyArtefact (IA ia) : base (ia)
	{
		artOffend = (ArtefactOffend)ia.desires [typeof(ArtefactOffend)];
	}

	public override void Run ()
	{

		base.Run ();

		ia.nav.speed = EnemyController.RUN_SPEED;

		SetSearching ();// If i'm carrying an Artefact, i'm not seraching for one

		if (!searching) {// If i'm not searching an Artefact
			ia.SetNavTarget (ia.player.team.teamSlot.receptor.transform.position);
			return;
		}

		SetTarget ();

		if (target != null) {
			ia.SetNavTarget (target.gameObject.transform.position);
		}
	}


	public void SetTarget ()
	{
		if (target == null || target.transporter != null) {
			target = null;
			Artefact art = FindAvailableArtefact ();
			if (art != null) {
				target = art;
			}
		}
	}

	public void SetSearching ()
	{
		searching = true;

		foreach (Artefact art in ia.gameManager.artefacts) {
			if (art.transporter == ia.player) {
				searching = false;
			}
		}
	}

	public Artefact FindAvailableArtefact ()
	{
		foreach (Artefact art in ia.gameManager.artefacts) {
			if (art.team.side != ia.player.side && art.transporter == null) {
				return art;
			}
		}
		return null; 
	}

	public override float EvaluatePriority ()
	{
		float val = artOffend.value;

		if (target != null && target.transporter == ia.player) {
			val += CARRRYING_ARTEFACT_VALUE;

		} else if (target == null) {
			Artefact art = FindAvailableArtefact ();
			if (art == null) {
				val -= NO_AVAILABLE_ARTEFACT_VALUE;
			}
		}
		return val;

	}

	public override void Setup ()
	{
		ia.nav.speed = EnemyController.RUN_SPEED;
	}

	public override void Reset ()
	{
		searching = true;
		target = null;
	}

}

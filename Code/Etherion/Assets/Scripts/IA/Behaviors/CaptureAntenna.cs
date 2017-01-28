using UnityEngine;
using System.Collections;

public class CaptureAntenna : IABehavior
{


	public static float GAIN_XP_COEFF = 0.07f;

	GainXP gainXP;
	AntennaPossesion antennaPossesion;

	Antenna target;

	public CaptureAntenna (IA ia) : base (ia)
	{
		gainXP = (GainXP)ia.desires [typeof(GainXP)];
		antennaPossesion = (AntennaPossesion)ia.desires [typeof(AntennaPossesion)];
	}

	public override void Run ()
	{
		SetTarget ();
	}

	public void SetTarget ()
	{

		if (target != null && target.owners == ia.player.team && target.capturePoints >= Antenna.CAPTURE_POINTS_TARGET) {
			target = null;
		}

		if (target == null) {
			 
			float prox = 50000f;

			foreach (Antenna a in ia.gameManager.antennas) {
				if (a.owners != ia.player.team || a.capturePoints < (9 * Antenna.CAPTURE_POINTS_TARGET / 10)) {
					float dist = Vector3.Distance (ia.gameObject.transform.position, a.gameObject.transform.position);
					if (dist < prox) {
						target = a;
						prox = dist;
					}
				}
			}
		}
		if (target != null) {
			ia.SetNavTarget (target.gameObject.transform.position);
		}
	}


	public override float EvaluatePriority ()
	{
		if (target != null && Vector3.Distance (ia.player.gameObject.transform.position, target.gameObject.transform.position) < 10f) {
			return 0f; // Maybe a little more ...
		}

		return antennaPossesion.value * gainXP.value * GAIN_XP_COEFF;
	}

	public override void Setup ()
	{
		ia.nav.speed = EnemyController.RUN_SPEED;
	}


	public override void Reset ()
	{
		target = null;
	}


}


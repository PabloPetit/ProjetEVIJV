using UnityEngine;
using System.Collections;

public class CaptureAntenna : IABehavior {


	public static float GAIN_XP_COEFF = 0.25f;

	GainXP gainXP;
	AntennaPossesion antennaPossesion;

	Antenna target; 

	public CaptureAntenna (IA ia) : base (ia)
	{
		gainXP = (GainXP)ia.desires [typeof(GainXP)];
		antennaPossesion = (AntennaPossesion)ia.desires [typeof(AntennaPossesion)];
	}

	public override void Run(){
		
	}

	public float 

	public override float EvaluatePriority ()
	{
		return antennaPossesion * gainXP * GAIN_XP_COEFF;
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


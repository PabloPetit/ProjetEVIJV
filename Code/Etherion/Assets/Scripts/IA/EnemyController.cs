using UnityEngine;
using System.Collections;

public class EnemyController : IA
{

	public static float RUN_SPEED = 15f;
	public static float WALK_SPEED = 8f;



	public override void SetDesires ()
	{
		
		desires.Add (typeof(Aggressivity), new Aggressivity (this));
		desires.Add (typeof(Cowardice), new Cowardice (this));
		desires.Add (typeof(ArtefactOffend), new ArtefactOffend (this));
		desires.Add (typeof(ArtefactDefend), new ArtefactDefend (this));

		//desires.Add (typeof(Discretion),new Discretion(this));
		//desires.Add (typeof(GainXP),new GainXP(this));


	}

	public override void SetBehaviors ()
	{
		behaviors.Add (new CaptureEnemyArtefact (this));
		behaviors.Add (new Attack (this));
		behaviors.Add (new DefendTeamArtefact (this));
	}

	public override void SetHeadAndBarrel ()
	{
		head = transform.Find ("Model/Head").gameObject;
		barrel = transform.Find ("Model/Head/RightHand/Gun/BarrelEnd").gameObject;
	}


}

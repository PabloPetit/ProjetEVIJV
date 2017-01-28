using UnityEngine;
using System.Collections;

public class EnemyController : IA
{

	public static float RUN_SPEED = 17f;
	public static float WALK_SPEED = 12f;


	public override void SetDesires ()
	{
		desires.Add (typeof(Aggressivity), new Aggressivity (this));
		desires.Add (typeof(Cowardice), new Cowardice (this));

		desires.Add (typeof(ArtefactOffend), new ArtefactOffend (this));
		desires.Add (typeof(ArtefactDefend), new ArtefactDefend (this));

		desires.Add (typeof(GainXP), new GainXP (this));
		desires.Add (typeof(AntennaPossesion), new AntennaPossesion (this));

	}

	public override void SetBehaviors ()
	{
		behaviors.Add (new CaptureEnemyArtefact (this));
		behaviors.Add (new Attack (this));
		behaviors.Add (new DefendTeamArtefact (this));
		behaviors.Add (new CaptureAntenna (this));
	}

	public override void SetHeadAndBarrel ()
	{
		head = transform.Find ("Model/Head").gameObject;
		barrel = transform.Find ("Model/Head/RightHand/Gun/BarrelEnd").gameObject;
	}


}

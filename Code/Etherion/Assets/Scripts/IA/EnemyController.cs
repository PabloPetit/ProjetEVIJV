using UnityEngine;
using System.Collections;

public class EnemyController : IA {

	public virtual void SetHeadAndBarrel(){

	}

	public override void SetDesires(){
		desires.Add (Discretion.NAME,new Discretion(this));
		desires.Add (Aggressivity.NAME,new Aggressivity(this));
		desires.Add (ArtefactDefend.NAME,new ArtefactDefend(this));
		desires.Add (ArtefactOffend.NAME,new ArtefactOffend(this));
		desires.Add (GainXP.NAME,new GainXP(this));
		desires.Add (Cowardice.NAME,new Cowardice(this));
	}

	public override void SetBehaviors(){
		behaviors.Add (new CaptureEnemyArtefact(this));
	}


}

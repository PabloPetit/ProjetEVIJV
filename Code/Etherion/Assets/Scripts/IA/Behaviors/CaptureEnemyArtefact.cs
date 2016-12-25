using UnityEngine;
using System.Collections;

public class CaptureEnemyArtefact : IABehavior {


	ArtefactOffend artOffend;

	bool searching;
	Artefact target;


	public CaptureEnemyArtefact(IA ia) : base (ia){
		name = "CaptureEnemyArtefact";
		artOffend = (ArtefactOffend)ia.desires [ArtefactOffend.NAME];
	}

	public virtual void Run(){
		SetSearching ();

		if(searching){
			if(target == null){
				if (SetTarget ()){
					ia.nav.SetDestination (target.gameObject.transform.position);
				}
			}// Else, behavior should end at the next iteration

		}else{
			ia.nav.SetDestination (ia.player.team.teamSlot.receptor.transform.position);
		}
	}

	public void SetSearching(){
		searching = true;

		foreach(Artefact art in ia.gameManager.artefacts){
			if(art.transporter != null && art.transporter == ia.player){
				searching = false;
			}
		}
	}

	public bool SetTarget(){
		target = null;
		foreach (Artefact art in ia.gameManager.artefacts) {
			if (art.team.side != ia.player.side && art.transporter == null){
				target = art; // Should be random ?
				break;
			}
		}
		return target != null;
	}

	public virtual bool endCondition(){
		return searching && target==null; // if i'm searching but there is nothing to find
	}

	public float EvaluatePriority(){
		return artOffend.value;
	}

	public virtual void Setup(){

	}

	public virtual void Reset(){
		searching = true;
		target = null;
	}

}

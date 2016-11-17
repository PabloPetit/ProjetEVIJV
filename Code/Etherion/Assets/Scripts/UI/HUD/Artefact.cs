using UnityEngine;
using System.Collections;

public class Artefact : MonoBehaviour {


	public float offset = 1.5f;
	Vector3 vOffset;

	public Team team;
	public Player transporter;
	public Transform spawn;
	Collider col;

	void Start () {
		col = GetComponent<Collider> ();
		vOffset = Vector3.up * offset;
	}

	void Update () {
	
		if(transporter == null){
			return;
		}
		if (transporter.health.dead){
			transporter = null;
		}else{
			transform.position = transporter.gameObject.transform.position + vOffset;
		}

	}

	void OnTriggerEnter(Collider other){

		if (transporter != null){
			return;
		}

		ArtefactReceptor receptor = other.GetComponent<ArtefactReceptor> ();

		if (receptor != null && transporter != null &&(receptor.team.side == transporter.team.side) ){
			//Send Score
			BackToBase ();
		}

		Player player = other.GetComponent<Player> ();

		if (player == null || player.isCreature){
			return;
		}

		if(player.side != team.side){
			BackToBase ();
		}else{
			transporter = player;
		}
			
	}

	void BackToBase(){
		transporter = null;
		transform.position = spawn.position;
	}
}

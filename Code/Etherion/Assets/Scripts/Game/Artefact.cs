using UnityEngine;
using System.Collections;

public class Artefact : MonoBehaviour
{


	public float offset = 1.5f;
	Vector3 vOffset;

	public Team team;
	public Player transporter;
	public Transform spawn;
	Collider col;

	EventName gameManagerEvent;

	void Start ()
	{
		col = GetComponent<Collider> ();
		vOffset = Vector3.up * offset;
		gameManagerEvent = new EventName (GameManager.GAME_MANAGER_CHANNEL);
	}

	void Update ()
	{
	
		if (transporter == null) {
			return;
		}
		if (transporter.health.dead) {
			transporter = null;
		} else {
			transform.position = transporter.gameObject.transform.position + vOffset;
		}

	}

	void OnTriggerEnter (Collider other)
	{

		ArtefactReceptor receptor = other.GetComponent<ArtefactReceptor> ();

		if (receptor != null && transporter != null && (receptor.team.side == transporter.team.side)) {
			InformScore ();
			BackToBase ();
		}

		if (transporter != null) {
			return;
		}

		Player player = other.GetComponent<Player> ();

		if (player == null || player.isCreature) {
			return;
		}

		if (player.side != team.side) {
			transporter = player;
		} else {
			BackToBase ();
		}
			
	}

	void InformScore ()
	{
		EventManager.TriggerAction (gameManagerEvent, new object[]{ GameManager.UPDATE_SCORE, transporter.team.side });
	}

	void BackToBase ()
	{
		transporter = null;
		if (Vector3.Distance (transform.position, spawn.position) > 3) {
			transform.position = spawn.position;
		}
	}
}

using UnityEngine;
using System.Collections;

public class TeamSlot : MonoBehaviour
{

	public GameObject humanPrefab;
	public GameObject aiPrefab;
	public GameObject artefactPrefab;

	public Team team;
	public PlayerSpawn[] playerSpawns;
	public ArtefactSpawn artefactSpawn;
	public GameObject receptor;

	// Use this for initialization
	void Awake ()
	{
		playerSpawns = GetComponentsInChildren<PlayerSpawn> ();
		artefactSpawn = GetComponentInChildren<ArtefactSpawn> ();
		receptor = GetComponentInChildren<ArtefactReceptor> ().gameObject;

	}
		

	public GameObject GetRandomSpawn ()
	{
		int i = Random.Range (0, playerSpawns.Length - 1);
		return playerSpawns [i].gameObject;
	}

}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	public static string GAME_MANAGER_CHANNEL;

	public static int UPDATE_SCORE = 1;
	public static int UPDATE_KILLS = 2;


	public static float RESPAWN_DELAY = 5f;

	private static GameManager gameManager;

	public static string[] iaLevelNames = new string[]{ "Easy", "Meduim", "Hard" };

	public int iaLevel = 1;

	public static int teamNumber = 2;
	public static int playerPerTeam = 5;

	public static int targetScore = 2;
	public static int targetKills = 10;
	public static int maxTime = 15;
	// Minutes

	public static bool scoreCondition = true;
	public static bool killsCondition = true;

	public static TeamSlot[] teamSlots;

	public List<Team> teams;

	public static float timer = 0f;

	public bool humanSet;

	EventName gameManagerEvent;
	EventName gameState;

	void Start ()
	{
		humanSet = false;
		gameState = new EventName (GameState.GAME_STATE_CHANNEL);

		InitTeamSlots ();
		GenerateTeams ();
		InitializePlayers ();
		InitializeArtefacts ();

	}

	void Update ()
	{
		timer += Time.deltaTime;
		CheckGameOver ();
		SendState ();
	}

	void CheckGameOver ()
	{
		
	}


	void OnEnable ()
	{
		gameManagerEvent = new EventName (GAME_MANAGER_CHANNEL);
		EventManager.StartListening (gameManagerEvent, new System.Action<object[]> (UpdateState));
	}

	void UpdateState (object[] param)
	{
		int perf = (int)param [0];
		int team = (int)param [0];

		if (perf == UPDATE_SCORE) {
			teams [team].score += 1;
		} else if (perf == UPDATE_KILLS) {
			teams [team].kills += 1;
		}
	}

	void OnDisable ()
	{
		EventManager.StopListening (gameManagerEvent);
	}

	void SendState ()
	{
		int[] scores = new int[teams.Count];
		for (int i = 0; i < teamNumber; i++) {
			scores [i] = teams [i].score;
		}

		EventManager.TriggerAction (gameState, new object[]{ scores, targetScore, maxTime * 60 - timer });
	}


	void GenerateTeams ()
	{
		teams = new List<Team> ();
		for (int i = 0; i < teamNumber; i++) { 
			CreateTeam (i);
		}
	}

	void CreateTeam (int i)
	{
		Team a = new Team (i, teamSlots [i]);
		teamSlots [i].team = a;
		teams.Add (a);
	}

	void InitializePlayers ()
	{
		foreach (TeamSlot ts in teamSlots) {
			for (int i = 0; i < playerPerTeam; i++) {
				GameObject player;
				if (!humanSet) {
					player = Instantiate (ts.humanPrefab);
					humanSet = true;
				} else {
					player = Instantiate (ts.aiPrefab);
				}
				player.transform.position = ts.GetRandomSpawn ().transform.position;
				Player p = player.GetComponent<Player> ();
				p.team = ts.team;
				p.side = p.team.side;
			}
		}
	}

	void InitializeArtefacts(){
		foreach (TeamSlot ts in teamSlots) {

			GameObject artefact = Instantiate (ts.artefactPrefab);
			artefact.transform.position = ts.artefactSpawn.transform.position;
			Artefact art = artefact.GetComponent<Artefact> ();
			art.team = ts.team;
			art.spawn = ts.artefactSpawn.transform;

			ArtefactReceptor receptor = ts.receptor.GetComponent<ArtefactReceptor> ();
			receptor.team = ts.team;
		}
	}

	void InitTeamSlots ()
	{
		teamSlots = (TeamSlot[])FindObjectsOfType (typeof(TeamSlot));
	}

	public static GameManager instance {
		get {
			if (!gameManager) {
				gameManager = FindObjectOfType (typeof(GameManager)) as GameManager;
				if (!gameManager) {
					Debug.LogError ("There needs to be one active GameManager script on a GameObject in your scene");
				}
			}
			return gameManager;
		}
	}

}

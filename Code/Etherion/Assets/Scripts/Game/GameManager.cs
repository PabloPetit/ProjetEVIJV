using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{

	public static string GAME_MANAGER_CHANNEL = "gameManager";

	public static int UPDATE_SCORE = 1;
	public static int UPDATE_KILLS = 2;

	public static int teamNumber = 2;
	public static int playerPerTeam = 10;

	public static int targetScore = 2;
	public static int targetKills = 10;
	public static int maxTime = 15;


	public static float RESPAWN_DELAY = 5f;

	private static GameManager gameManager;

	public static string[] iaLevelNames = new string[]{ "Easy", "Meduim", "Hard" };

	public int iaLevel = 1;


	public static bool scoreCondition = true;
	public static bool killsCondition = true;

	public static TeamSlot[] teamSlots;

	public List<Team> teams;

	public List<Artefact> artefacts;

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
		InitializeArtefacts ();
		InitializePlayers ();


	}

	void Update ()
	{
		timer += Time.deltaTime;
		CheckGameOver ();
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
		int team = (int)param [1];

		if (perf == UPDATE_SCORE) {
			teams [team].score += 1;
			SendScore ();
		} else if (perf == UPDATE_KILLS) {
			teams [team].kills += 1;
		}
	}

	void OnDisable ()
	{
		EventManager.StopListening (gameManagerEvent);
	}


	void SendScore ()
	{	
		EventName scoreEvent = new EventName (Score.SCORE_CHANNEL);
		int[] scores = new int[teams.Count];
		for (int i = 0; i < teamNumber; i++) {
			scores [i] = teams [i].score;
		}

		EventManager.TriggerAction (scoreEvent, new object[]{ scores });
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
					EnemyController enemiController = player.GetComponent<EnemyController> ();
				}

				Player p = player.GetComponent<Player> ();

				player.transform.position = ts.GetRandomSpawn ().transform.position;
				ConfigureNaVAgent (player,p);
				
				p.team = ts.team;
				p.side = p.team.side;
			}
		}
	}

	void ConfigureNaVAgent(GameObject player, Player p){
		
		if(p.isHuman){
			return;
		}

		player.AddComponent (typeof(NavMeshAgent));
		NavMeshAgent nav = player.GetComponent<NavMeshAgent> ();
		nav.baseOffset = 1.5f;
	}

	void InitializeArtefacts ()
	{
		foreach (TeamSlot ts in teamSlots) {

			GameObject artefact = Instantiate (ts.artefactPrefab);
			artefact.transform.position = ts.artefactSpawn.transform.position;
			Artefact art = artefact.GetComponent<Artefact> ();
			art.team = ts.team;
			art.spawn = ts.artefactSpawn.transform;
			ts.artefact = art;
			ArtefactReceptor receptor = ts.receptor.GetComponent<ArtefactReceptor> ();
			receptor.team = ts.team;
			artefacts.Add (art);
		}
	}

	void InitTeamSlots ()
	{
		teamSlots = (TeamSlot[])FindObjectsOfType (typeof(TeamSlot));
		TeamSlot a = teamSlots [0];
		TeamSlot b = teamSlots [1];
		teamSlots [0] = b;
		teamSlots [1] = a;
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

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Team
{
	public int side;
	public int score;
	public int kills;
	public List<Player> players;

	public GameObject artefact;
	public TeamSlot teamSlot;

	public Team (int side, TeamSlot teamSlot)
	{
		this.side = side;
		this.teamSlot = teamSlot;
		this.score = 0;
		this.kills = 0;
		this.players = new List<Player> ();
	}

}

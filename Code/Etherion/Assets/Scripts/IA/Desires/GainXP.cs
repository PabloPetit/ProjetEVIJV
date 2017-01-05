using UnityEngine;
using System.Collections;

public class GainXP : Desire
{

	public static string NAME = "GAIN_XP";

	public static float STD_MIN_VALUE = 15f;

	public GainXP (IA ia) : base (ia)
	{
		this.MIN_VALUE = STD_MIN_VALUE * personalCoeff;
	}

	public override void Setup ()
	{
		
	}

	public override void Update ()
	{
		
	}

	float EnemyMeanLevel(){

		float level = 0;
		foreach(Team t in ia.gameManager.teams){
			if (t != ia.player.team) {
				foreach (Player p in t) {
					level+= 	
				}
			}
		}

	}

}

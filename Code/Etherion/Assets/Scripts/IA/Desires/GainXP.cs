using UnityEngine;
using System.Collections;

public class GainXP : Desire
{
	
	public static float STD_MIN_VALUE = 10f;

	public static float LEVEL_DIFF_VALUE = 5f;

	public GainXP (IA ia) : base (ia)
	{
		this.MIN_VALUE = STD_MIN_VALUE * personalCoeff;
	}
		

	public override void Update ()
	{
		float enemyVal = Mathf.Max(EnemyMeanLevel () - ia.player.experience.level, 0f);
		enemyVal *= LEVEL_DIFF_VALUE * personalCoeff;

		value = this.MIN_VALUE + enemyVal;
	}

	float EnemyMeanLevel(){
		
		float level = 0;
		float count = 0;
		foreach(Team t in ia.gameManager.teams){
			if (t != ia.player.team) {
				foreach (Player p in t.players) {
					level += p.experience.level;
					count++;
				}
			}
		}

		return level/count;
	}

}

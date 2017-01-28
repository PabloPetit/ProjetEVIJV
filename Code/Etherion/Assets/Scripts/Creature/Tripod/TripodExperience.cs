using UnityEngine;
using System.Collections;

public class TripodExperience : Experience
{

	public override float RetrievedXp ()
	{
		return 2000f + level * 100 + totalXp / 10;
	}
}

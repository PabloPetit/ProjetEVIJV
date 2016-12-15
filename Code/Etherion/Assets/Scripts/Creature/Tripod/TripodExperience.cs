using UnityEngine;
using System.Collections;

public class TripodExperience : Experience
{

	public override float RetrievedXp ()
	{
		return 100 + level * 100 + totalXp / 10;
	}
}

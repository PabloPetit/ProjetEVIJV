using UnityEngine;
using System.Collections;

public class TripodExperience : Experience
{

	public override float RetrievedXp ()
	{
		return 5000 + level * 100 + totalXp / 10;
	}
}

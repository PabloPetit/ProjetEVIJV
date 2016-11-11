using UnityEngine;
using System.Collections;

public interface IDamage
{

	Player shooter { get ; set ; }

	bool hitMarker { get ; set ; }

	bool friendlyFire{ get ; set ; }

	float initialDamage { get ; set ; }

	float minDamage { get ; set ; }

	float damageDecrease { get ; set ; }

}
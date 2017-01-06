using UnityEngine;
using System.Collections;

public class AntennaPossesion : Desire {


	public static float MAX_DISTANCE = 500f;

	public static float STD_MIN_VALUE = 25f;

	public AntennaPossesion (IA ia) : base (ia)
	{
		this.MIN_VALUE = STD_MIN_VALUE * personalCoeff;
	}

	public override void Update ()
	{
		value = MIN_VALUE;

		Antenna closestOk = null;
		float prox = MAX_DISTANCE + 1f;
		int count = 0;

		foreach(Antenna a in ia.gameManager.antennas){

			if(a.owners != ia.player.team || a.capturePoints < ( 9 * Antenna.CAPTURE_POINTS_TARGET / 10 ) ){
				count++;
				float dist = Vector3.Distance (ia.gameObject.transform.position, a.gameObject.transform.position);
				if ( dist < prox){
					closestOk = a;
					prox = dist;
				}
			}
		}

		value += Mathf.Max (0f, MAX_DISTANCE - prox) / 10f;
		value *= Mathf.Log (1 + count);

	}


}

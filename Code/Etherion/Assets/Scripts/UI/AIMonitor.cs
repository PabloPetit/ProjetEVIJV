using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AIMonitor : MonoBehaviour
{

	// Use this for initialization

	public IA ia;
	public bool find = true;

	Text text;

	void Start ()
	{
		text = GetComponentInChildren<Text> ();
		//ia = FindObjectOfType<EnemyController> ();
		ia = FindObjectOfType<TripodController> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
		if (ia != null) {

			string str = "IA MONITOR : " + ia.name + " \n\n";

			if (ia.nav.isActiveAndEnabled && false) {

				str += "Nav : Dest " + ia.navTarget.ToString () + "\nTemp " + ia.nav.destination +
				"\nSpeed : " + ia.nav.speed + "  Remaining : " + ia.nav.remainingDistance + "\n\n";
			}

			foreach (IABehavior b in ia.behaviors) {
				str += b.GetType ().ToString () + " : " + b.EvaluatePriority ().ToString ("F1") + "\n";
			}
			str += "\n";
			foreach (Desire d in ia.desires.Values) {
				str += d.GetType ().ToString () + " : " + d.value.ToString ("F1") + " - [ P : " + d.personalCoeff.ToString ("F1") + "  m : " + d.MAX_VALUE.ToString ("F1") + "  M : " + d.MIN_VALUE.ToString ("F1") + " ]" + "\n";
			}

			str += "\n";

			str += "EnmyAround Count : " + ia.enemiesAround.Count + "\n";
			str += "CreaAround Count : " + ia.creaturesAround.Count + "\n";
			str += "AlliAround Count : " + ia.friendsAround.Count + "\n";

			text.text = str;

		} else if (find) {
			ia = FindObjectOfType<EnemyController> ();
		}

	}
}

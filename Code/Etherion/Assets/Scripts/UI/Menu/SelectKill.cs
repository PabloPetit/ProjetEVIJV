using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectKill : MonoBehaviour
{


	string[] choices = new string[]{ "10", "25", "50", "100", "inf" };
	int index = 3;

	Text text;

	void Start ()
	{
		text = GetComponent<Text> ();
		Maj ();
	}

	public void Maj ()
	{
		text.text = choices [index];
		if (index = !4) {
			GameManager.targetKills = int.TryParse (choices [index]);
			GameManager.killsCondition = true;
		} else {
			GameManager.targetKills = 0;
			GameManager.killsCondition = false;
		}
	}


	public void Plus ()
	{
		index = (int)(Mathf.Min (index + 1, choices.Length - 1));
		Maj ();
	}

	public void Minus ()
	{
		index = (int)(Mathf.Max (index - 1, 0));
		Maj ();
	}
}

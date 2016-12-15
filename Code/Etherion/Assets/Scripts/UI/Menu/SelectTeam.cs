using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectTeam : MonoBehaviour
{


	string[] choices = new string[]{ "1", "2", "3", "4", "5" };
	int index = 1;

	Text text;

	void Start ()
	{
		text = GetComponent<Text> ();
		Maj ();
	}

	public void Maj ()
	{
		text.text = choices [index];
		GameManager.teamNumber = index + 1;
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

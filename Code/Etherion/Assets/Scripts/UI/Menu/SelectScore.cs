using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectScore : MonoBehaviour
{


	string[] choices = new string[]{ "1", "5", "10", "25", "inf" };
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
		GameManager.targetScore = int.TryParse(choices[index])
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

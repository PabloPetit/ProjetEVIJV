using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectTime : MonoBehaviour
{


	string[] choices = new string[]{ "15 min", "20 min", "30 min", "inf" };
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
		//GameManager.DoStuff()
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

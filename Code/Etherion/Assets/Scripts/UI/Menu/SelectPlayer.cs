﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectPlayer : MonoBehaviour
{


	string[] choices = new string[]{ "1", "2", "5", "10", "15" };
	int index = 4;

	Text text;

	void Start ()
	{
		text = GetComponent<Text> ();
		Maj ();
	}

	public void Maj ()
	{
		text.text = choices [index];
		GameManager.playerPerTeam = int.Parse (choices [index]);
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

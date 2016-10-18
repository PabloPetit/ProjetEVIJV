using UnityEngine;
using System.Collections;

public class SkratHealth : PlayerHealth {

	// Use this for initialization
	void Start () {
		maxLife = 100f;
		timeBeforeAutoCure = 3f;
		autoCureValue = 10f;
		InitCommonFields ();
	}

}

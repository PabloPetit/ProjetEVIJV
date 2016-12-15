using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour {

	public static int MAX_LEVEL = 10;

	public float totalXp;
	public int level;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void gainXP(float xp){

		totalXp += xp;


	}
}

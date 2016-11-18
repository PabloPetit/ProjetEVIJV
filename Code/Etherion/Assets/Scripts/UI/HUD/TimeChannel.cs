using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeChannel : MonoBehaviour
{

	Text text;
	public float timer = 0.0f;
	public float maxTime;

	public int remainingMin;
	public int remainingSec;



	// Use this for initialization
	void Start ()
	{
		text = GetComponentInChildren<Text> ();
		maxTime = GameManager.maxTime * 60;

		remainingMin =  GameManager.maxTime;
		remainingSec = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		text.text = (int)((maxTime - timer) / 60) + " : " + (int)((maxTime - timer) % 60);
	
	}
}

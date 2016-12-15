using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Fps : MonoBehaviour
{

	Text text;

	public int refreshTime;

	int counter;

	void Start ()
	{
		text = GetComponent<Text> ();
		counter = 0;
	}
	
	// Update is called once per frame
	void Update ()
	{
		counter++;
		if (counter >= refreshTime) {
			text.text = "FPS : " + (int)(1f / Time.deltaTime);
			counter = 0;
		}

	}
}

using UnityEngine;
using System.Collections;

public class ControlerFooIA : MonoBehaviour
{

	public GameObject dest;

	UnityEngine.AI.NavMeshAgent nav;

	Animator anim;

	void Start ()
	{
	
		nav = GetComponentInChildren<UnityEngine.AI.NavMeshAgent> ();
		nav.SetDestination (dest.transform.position);
		anim = GetComponentInChildren<Animator> ();
		anim.SetTrigger ("Run");
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

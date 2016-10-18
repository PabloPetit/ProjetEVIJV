using UnityEngine;
using System.Collections;

public class Interaction : MonoBehaviour {

	GameObject initiator;

	public void Interact(GameObject initiator){
		this.initiator = initiator;
		Action ();
	}

	public virtual void Action(){
	}

}

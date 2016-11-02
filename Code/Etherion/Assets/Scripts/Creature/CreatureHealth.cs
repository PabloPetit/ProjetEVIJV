using UnityEngine;
using System.Collections;

public class CreatureHealth : MonoBehaviour {

	public float maxLife;
	protected float life;
	public bool dead;


	public float timeBeforeAutoCure;
	public float autoCureValue;
	protected float timer;

	public float defense = 1f; // Do not set it to zero 

	// Use this for initialization
	public void Start () {
		life = maxLife;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= timeBeforeAutoCure && !dead && life != maxLife) {
			life += Time.deltaTime * autoCureValue;
		}
	}

	public void TakeDamage(float damage, GameObject assailant){
		life -= damage / defense;
		if (life <= 0 && !dead) {
			dead = true;
			Death ();
		}
	}

	public virtual void Death(){}
}

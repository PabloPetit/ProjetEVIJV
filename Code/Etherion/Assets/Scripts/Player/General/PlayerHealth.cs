using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float maxLife;
	public float life;
	public bool invincible;
	public bool dead;

	public float timeBeforeAutoCure;
	public float autoCureValue;
	float timer;


	public void InitCommonFields(){
		life = maxLife;
		invincible = false;
		dead = false;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer >= timeBeforeAutoCure && !dead && life != maxLife) {
			life += Time.deltaTime * autoCureValue;
		}
	}

	public void TakeDamage(float damage){
		if (!invincible && !dead)
			return;
		
		life = Mathf.Max (0f, life - damage);

		timer = 0f;

		if (life == 0) {
			Die ();
		}

	}

	public void Die(){
		dead = true;

	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

	public float maxLife;
	public float life;
	public bool dead;

	public float timeBeforeAutoCure;
	public float autoCureValue;
	float timer;
	PlayerState state;

	Slider lifeSlide;
	RawImage bloodCanvas;


	void Start(){
		state = GetComponent<PlayerState> ();
		lifeSlide = GameObject.Find ("LifeSlider").GetComponent<Slider> ();
		bloodCanvas = GameObject.Find ("BloodCanvas").GetComponent<RawImage> ();
		life = maxLife;
		dead = false;
		timer = 0f;
	}
		
		
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		lifeSlide.value = life / maxLife;

		Color c = bloodCanvas.color;
		c.a = 1f - life / maxLife;
		bloodCanvas.color = c;

		if (timer >= timeBeforeAutoCure && !dead && life != maxLife) {
			life += Time.deltaTime * autoCureValue;
		}
	}

	public void TakeDamage(float damage, int side){
		if (state.indestructible || dead) {
			return;
		}
			
		
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

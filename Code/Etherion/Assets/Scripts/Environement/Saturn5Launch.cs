using UnityEngine;
using System.Collections;

public class Saturn5Launch : MonoBehaviour {

	AudioSource countdown;
	AudioSource thruters;
	float timer;
	float speed;
	float rotationSpeed = 0.1f;
	float acceleration = 10f;
	float start = 11f;

	public bool initiated;
	public bool launched1;
	public bool launched2;
	ParticleSystem fire;

	void Start () {
		countdown = transform.Find ("pCylinder1").gameObject.GetComponent<AudioSource> ();
		thruters =  transform.Find ("polySurfa4").gameObject.GetComponent<AudioSource> ();
		fire = GetComponentInChildren<ParticleSystem> ();
		timer = 0f;
		speed = 0f;
		launched1 = false;
		launched2 = false;
		initiated = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (!initiated)
			return;

		timer += Time.deltaTime;

		if (timer > 47) {
			Destroy (gameObject);
		}

		if (timer > start-5 && !launched1) {
			countdown.Play ();
			launched1 = true;
		}
		if (timer > start ) {
			if (!launched2) {
				thruters.Play ();
				launched2 = true;
				fire.Play ();
			}

			transform.position += Vector3.up * (speed * Time.deltaTime);
			speed += acceleration * Time.deltaTime;

		}
	}
}

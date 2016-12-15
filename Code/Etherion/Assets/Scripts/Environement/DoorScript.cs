using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour
{

	Animation anim;

	CapsuleCollider caps;

	float animTime = 2f;

	float timer;
	bool open;

	int peopleInside;

	Vector3 point1;
	Vector3 point2;
	int mask;

	// Use this for initialization
	void Start ()
	{
		anim = transform.parent.Find ("door").GetComponent<Animation> ();
		caps = GetComponent<CapsuleCollider> ();
		point1 = caps.transform.position + caps.transform.forward * (caps.height / 2);
		point2 = caps.transform.position - caps.transform.forward * (caps.height / 2);
		timer = 0f;
		open = false;
		mask = LayerMask.GetMask ("Player");
	}
	
	// Update is called once per frame
	void Update ()
	{
		timer += Time.deltaTime;

		if (open && timer > animTime) {

			Collider[] cols = Physics.OverlapCapsule (point1, point2, caps.radius, mask);

			if (cols.Length == 0) {

				anim.Play ("close");
				timer = 0f;
				open = false;
			}
		}
	}

	void OnTriggerEnter (Collider other)
	{
		Player player = other.gameObject.GetComponent<Player> ();
		if (player != null && timer > animTime && !open) {
			timer = 0f;
			open = true;
			anim.Play ("open");
		}
	}
}

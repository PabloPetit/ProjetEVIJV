using UnityEngine;
using System.Collections;

public class SkratBullet : Projectile {

	MeshRenderer render;

	int frameDelay = 1;
	int frame;

	protected override void endStart () {
		initialDamage = 2f;
		damageDecrease = 0f;
		speed = 500f;
		range = 1000f;
		render = transform.Find ("VolumetricLinePrefab").gameObject.GetComponent<MeshRenderer> ();
		//render.enabled = false;
		//frame = 0;
	}

	void endUpdate(){

		/*
		if (frame > frameDelay){
			render.enabled = true;
		}
		frame++;
		*/
	}


}

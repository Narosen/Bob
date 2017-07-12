using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilWizard : Enemies {


	public GameObject spell;
	public float secondsBetweenSpawn=1.0f;
	public Transform shootingPoint;
	public static int Damage=3;




	void Update(){

		if (health > 0) {
			if (facingRight)
				flipFlag = 1;
			else
				flipFlag = -1;

			if (playerSpoted) {
				if (Vector3.Distance (GameManager.gm.player.transform.position, transform.position) > minDis) { 
					
				} else {
					shootingPoint.LookAt (GameManager.gm.player.transform);


				}
				if (playerPunch == true) {
					StartCoroutine (BeingHit ());
				}
				playerPunch = false;

			}
		} else {
			death ();
		}



	}


	void FixedUpdate () {

		playerSpoted = Physics2D.OverlapCircle (transform.position, playerCheckRaduis, player);
		anim.SetBool ("wizardArea", playerSpoted);

		if (playerSpoted) {
			if (Time.time % secondsBetweenSpawn == 0) {
				GameObject power = Instantiate (spell, shootingPoint.position, shootingPoint.localRotation) as GameObject;
				power.transform.parent = gameObject.transform;

			}
			if ( PlayerController.positionX < transform.position.x && facingRight)
				Flip ();
			else if (PlayerController.positionX > transform.position.x && !facingRight)
				Flip ();
		}

	}



}

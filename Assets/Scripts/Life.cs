using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Life : MonoBehaviour {
	
	public AudioSource lifeCollect;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			lifeCollect.Play ();
			PlayerController.Health += 10;
			Destroy (gameObject);
		}
	}
}

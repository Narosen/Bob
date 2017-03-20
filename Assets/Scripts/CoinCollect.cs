using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollect : MonoBehaviour {

	public AudioSource collectSound;

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player") {
			collectSound.Play ();
			GameManager.gm.score += 1;
			Destroy (gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForAnimation : MonoBehaviour {


	public static Animator anim;

	// Use this for initialization
	void Update () {
		if (anim == null) {
			anim = GetComponent<Animator> ();
		}

	}
	

}

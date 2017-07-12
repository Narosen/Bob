using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserNotice : MonoBehaviour {

	public string buttonName;

	// Update is called once per frame
	void Update () {

		if (Input.GetButtonDown (buttonName)) {
			this.gameObject.SetActive (false);
		}
		
	}
}

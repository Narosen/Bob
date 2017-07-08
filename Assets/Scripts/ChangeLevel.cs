using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

	public string levelToLoad;

	public void OnCollisionEnter2D(Collision2D coll){
		if (coll.transform.tag == "Player") {
			SceneManager.LoadScene (levelToLoad);
		}

	}

	public void loadLevel(){
		SceneManager.LoadScene (levelToLoad);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLevel : MonoBehaviour {

	public string levelToLoad;

	public void loadLevel(){
		SceneManager.LoadScene (levelToLoad);
	}
}

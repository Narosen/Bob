using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

	// make game manager public static so can access this from other scripts
	public static GameManager gm = null;

	public GameObject player;
	public enum GameState
	{
		Playing,
		Menu,
		Lose
	};

	public int score=0;

	public Text points;
	public Text healthPercentage;

	public Image HealthBar;


	public GameObject gameCanvas;
	public GameObject gameOver;
	public GameObject playAgainButtons;

	public GameObject exitButtons;

	public AudioSource musicAudioSource;
	public AudioSource gameOverMusic;
	public AudioSource button;

	public string mainMenuScreen;


	public GameState gamestate = GameState.Playing;

	private bool pause = false;


	// setup the game
	void Start () {
		
		
		musicAudioSource.Play ();

		if (gm == null)
			gm = this.gameObject.GetComponent<GameManager> ();

				
		gm.gamestate = GameState.Playing;
		points.text = "0";
		healthPercentage.text = "100%";


		if (gameOver)
			gameOver.SetActive (false);
		

	}

	// this is the main game event loop
	void Update () {
		
		float curHealth=PlayerController.Health/100f;
		HealthBar.transform.localScale= new Vector3 (Mathf.Clamp (curHealth,0f,1f), HealthBar.transform.localScale.y,  HealthBar.transform.localScale.z);

		if (Input.GetKey ("escape")) {
			pause = true;
			Time.timeScale = 0;
		};
		switch (gamestate) {
			
		case GameState.Playing:
			
			healthPercentage.text = PlayerController.Health + "%";
			points.text = score + "";
			break;

		case GameState.Menu:
			
			break;
		
		case GameState.Lose:
			musicAudioSource.Stop ();
			gameOverMusic.Play ();
			gameOver.SetActive (true);
			break;
			
		}
	}

	void OnGUI(){

		if(pause == true)
		{
			if(GUI.Button(new Rect(Screen.width /2 - 100 , Screen.height /2 + 25 ,250,25), "Main Menu"))
			{
				button.Play ();
				Application.LoadLevel (mainMenuScreen);

			}
			if(GUI.Button(new Rect(Screen.width /2 - 100 , Screen.height /2 - 25 ,250,25), "Resume"))
			{
				if(pause == true)
				{
					//unpause the game
					button.Play();
					pause = false;
					Time.timeScale = 1;

				}

			}
		}
		
	}
}

		

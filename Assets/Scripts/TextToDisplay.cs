using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextToDisplay : MonoBehaviour {

	[SerializeField] private TextAsset messages;
	[SerializeField] private AudioSource[] soundEffects =null;
	private int[] pointOfEffects = { 15, 18, 20, 24, 31 };
	private int[] noOfCommands = { 3, 3, 1 , 4, 1 };
	private CutScenesCommand command15, command15_1, command15_2, command18, command18_1, command18_2, command20, command24, command24_1,
	command24_2, command24_3,command31;

	public CutSceneManager manager;
	public Transform Playertrans;
	public GameObject Player;
	public Rigidbody2D rb;
	public Transform target1;
	public Transform target2;
	public GameObject[] objectToChange;
	public GameObject[] changedObject;





	void Start(){


		manager = FindObjectOfType<CutSceneManager> ();
		command15 = new PlayAudio (soundEffects[0]);
		command15_1 = new ShockJump (rb);
		command15_2 = new Flip (Playertrans,-1);
		command18 = new Move (rb,target1);
		command18_1 = new Exchange (objectToChange[0],changedObject[0]);
		command18_2 = new PlayAudio (soundEffects[1]);
		command20 = new SetActiveTill (changedObject[0]);
		command24 = new Flip (Playertrans,-1);
		command24_1 = new Move (rb,target2);
		command24_2 = new Exchange (objectToChange[1],null);
		command24_3 = new PlayAudio (soundEffects[2]);
		command31 = new PlayAudio (soundEffects[3]);

	}

	void OnTriggerEnter2D(Collider2D other) {


		if (other.tag == "Player") {


			manager.TakeResources (messages, pointOfEffects, noOfCommands);
			CutSceneManager.Commands.Add (command15);
			CutSceneManager.Commands.Add (command15_1);
			CutSceneManager.Commands.Add (command15_2);
			CutSceneManager.Commands.Add (command18);
			CutSceneManager.Commands.Add (command18_1);
			CutSceneManager.Commands.Add (command18_2);
			CutSceneManager.Commands.Add (command20);
			CutSceneManager.Commands.Add (command24);
			CutSceneManager.Commands.Add (command24_1);
			CutSceneManager.Commands.Add (command24_2);
			CutSceneManager.Commands.Add (command24_3);
			CutSceneManager.Commands.Add (command31);
			manager.isActive = true;

			Destroy (gameObject);
		
		}

	}
}

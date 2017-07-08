using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneManager : MonoBehaviour {

	//the textManager will contain the properties of the dialog box.
	//but the texts and actions(resources) will be contained in the NPCs
	//when the player triggers them, the texts is send here and the dialog box is activated.

	//properties of the dialog box
	[SerializeField] private GameObject dialogBox;
	[SerializeField] private float typingSpeed = 0.1f;
	private bool isTyping = false;
	private int currentLine = 0; //for textLines[]
	private int currentEffect = 0; //for pointOfEffects[]
	private int currentCommand = 0; //for List Commands
	private int noCurrentCommand = 0; //for noOfCommands[]
	public Text theText;
	private bool cancelTyping;
	public GameObject player;
	public static PlayerController pc;
	public GameObject changeScene;


	//resources from the NPCs
	public int[] pointOfEffects;
	public bool isActive = false; 
	public static List<CutScenesCommand> Commands = new List<CutScenesCommand>();
	public static int[] noOfCommands;

	//determined by the resources given
	private int endOfLine;
	public string[] textLines;

	void Start(){

		pc = player.GetComponent<PlayerController> ();
		if (isActive) {
			EnableDialogBox ();
		} else {
			DisableDialogBox ();
		}

	}

	void Update(){

		if (!isActive) {
			
			return;
		} 


		if(Input.GetKeyDown(KeyCode.Return)){

			pc.enabled = false;
			//if the text is no longer typing
			if (!isTyping) {

				if (currentLine > endOfLine) {
					changeScene.SetActive (true);
					DisableDialogBox ();
					pc.enabled = true;

				} else {
					if (currentEffect < pointOfEffects.Length && currentLine == pointOfEffects [currentEffect]) {
							InitiateEffect ();
							noCurrentCommand++;
							currentEffect++;
							Debug.Log ("noCurrentCommand: "+noCurrentCommand);
							Debug.Log ("currentEffect: "+currentEffect);
							
					
					}else {
						Debug.Log ("Line" + currentLine);
						EnableDialogBox ();
						}

				}

				currentLine += 1;

			}
			//if the text is typng but wants to skip typing
			else if(isTyping && !cancelTyping){
				cancelTyping = true;
			}
		}


	}

	public void TakeResources(TextAsset Rmessages, int[] RpointOfEffects, int[] RnoOfCommands){

		pointOfEffects = RpointOfEffects;
		noOfCommands = RnoOfCommands;
		if (textLines != null) {
			textLines = new string[1];
		
		}

		textLines = Rmessages.text.Split ('\n');
		endOfLine = textLines.Length - 1;
	
	}

	public void EnableDialogBox (){

		dialogBox.SetActive (true);
		isActive = true;

		StartCoroutine (TextScroll (textLines [currentLine]));
	}

	private IEnumerator TextScroll (string lineOfText){

		int letter = 0;
		theText.text = "";
		isTyping = true;
		cancelTyping = false;
		while (isTyping && !cancelTyping && (letter < lineOfText.Length - 1)) {
			theText.text += lineOfText [letter];
			letter += 1;
			yield return new WaitForSeconds(typingSpeed);
		}
		theText.text = lineOfText;
		isTyping = false;
		cancelTyping = false;

	}

	public void DisableDialogBox (){
		dialogBox.SetActive (false);
		isActive = false;

	}

	void InitiateEffect(){
		DisableDialogBox ();
		if (Commands.Count > 0 && noOfCommands[noCurrentCommand] > 0 )
		{	int totalCommands = noOfCommands[noCurrentCommand] + currentCommand;
			StartCoroutine(Play(totalCommands));
		}
	}

	IEnumerator Play(int totalCommands){
	
		while (currentCommand < (totalCommands))
		{
			//Move the box with the current command
			yield return StartCoroutine(Commands[currentCommand].Execute());
			Debug.Log ("currentCommand: "+currentCommand);
			currentCommand++;
			PlayerController.anim.SetFloat ("Speed", 0.0f);


		}


		isActive = true;
		Debug.Log ("Line" + currentLine);
		EnableDialogBox ();
		currentLine += 1;
	}

}

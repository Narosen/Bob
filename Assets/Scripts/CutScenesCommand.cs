using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CutScenesCommand  {

	//speed of the character
	public float speed = 1.0f;
	//jump force in case jump is present
	public float jumpForce = 1.5f;
	//Move and maybe save command
	protected static float flip = 1f; //1 is for right and -1 is for left

	public abstract IEnumerator Execute ();


	}


//
// Child classes
//

//to play the audio sound Effect
public class PlayAudio : CutScenesCommand
{
	private AudioSource audio;

	public PlayAudio(AudioSource audio){ //we should assign the given audio source while initializing;
		this.audio = audio;
	}

	//Called when we press a key
	public override IEnumerator Execute()
	{
		//Move the box
		audio.Play();
		yield return new WaitForSeconds (0.5f);

	}

}

//this is for idle command
public class SetActiveTill : CutScenesCommand
{	
	GameObject a;

	public SetActiveTill(GameObject a){
		this.a = a;
	}
	
	public override IEnumerator Execute()
	{

		Enemies e = a.GetComponent<Enemies> ();
		CutSceneManager.pc.enabled = true;
		while (e.isDeath != true) {
			yield return null;
		} 
		CutSceneManager.pc.enabled = false;
		yield return null;

	}


}


//moving the character to a particular target
public class Move : CutScenesCommand
{
	Rigidbody2D rb;
	Transform target;

	public Move(Rigidbody2D rb,Transform target){
		this.rb = rb;
		this.target = target;

	}

	public override IEnumerator Execute()
	{
		if ((PlayerController.positionX < target.position.x) && (PlayerController.facingRight!=1f && flip <= -1f)) {
		
			flip *= -1;
			Vector3 theScale = rb.transform.localScale;
			theScale.x *= -1;
			rb.transform.localScale = theScale;
			PlayerController.facingRight = flip;
		}
		else if ((PlayerController.positionX > target.position.x) && (PlayerController.facingRight==1f) && flip >= 1f){

			flip *= -1;
			Vector3 theScale = rb.transform.localScale;
			theScale.x *= -1;
			rb.transform.localScale = theScale;
			PlayerController.facingRight = flip;
		}
			
		while (Vector2.Distance (rb.position, target.position) > 0.3f) {
			Debug.Log (flip);
			rb.velocity = new Vector2 (flip * speed, rb.velocity.y);
			PlayerController.anim.SetFloat ("Speed", Mathf.Abs (rb.velocity.x));

			yield return null;
		} 

		yield return null;



	}


}


//For keys with no binding
public class Flip : CutScenesCommand
{
	Transform trans;
	int toFlip;

	public Flip(Transform trans,int x){
		this.trans = trans;
		this.toFlip = x;
	}

	public override IEnumerator Execute()
	{
		flip =(int) PlayerController.facingRight;
		if (flip != toFlip) {
			flip *= -1;
			Vector3 theScale = trans.localScale;
			theScale.x *= -1;
			trans.localScale = theScale;
			PlayerController.facingRight = flip;


		} 

		yield return new WaitForSeconds (0.5f);


	}

}



//Replay all commands
public class ShockJump : CutScenesCommand
{
	Rigidbody2D rb;

	public ShockJump(Rigidbody2D rb){
		this.rb = rb;
	}

	public override IEnumerator Execute()
	{
		rb.velocity = new Vector2 (rb.velocity.x, jumpForce);
		yield return new WaitForSeconds (0.5f);
	}


}

//Replay all commands
public class Exchange : CutScenesCommand
{
	GameObject a;
	GameObject b;

	public Exchange(GameObject a,GameObject b){
		this.a = a;
		this.b = b;
	}

	public override IEnumerator Execute()
	{
		a.SetActive (false);
		b.SetActive (true);
		yield return null;

	}


}

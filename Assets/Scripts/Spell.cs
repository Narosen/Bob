using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour {

	public float speed=3.0f;
	private int flip;
	private int damage = EvilWizard.Damage;
	private EvilWizard evil;
	public float timeOut = 2.0f;


	// Use this for initialization
	void Awake () {
		// invote the DestroyNow funtion to run after timeOut seconds
		Invoke ("DestroyNow", timeOut);
	}


	void DestroyNow ()
	{
		// destory the game Object
		Destroy(gameObject);
	}
	void Start(){
		transform.Rotate(new Vector3(0f, 270f, 0f));
		evil = gameObject.GetComponentInParent<EvilWizard> ();

		if (evil.facingRight)
			flip = 1;
		else
			flip = -1;
		
	}

	// Update is called once per frame
	void Update () {
		

		transform.Translate (Vector3.right * Time.deltaTime*speed*flip);


	}

	void OnCollisionEnter2D(Collision2D coll){
		if (coll.transform.tag== "Player") {
			PlayerController.Health -= damage;
			Destroy (gameObject);
		}
	}


}

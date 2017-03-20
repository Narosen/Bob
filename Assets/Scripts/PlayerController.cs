using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 10.0f;
	public float jumpForce=175f;
	public float groundRaduis=0.2f;
	public float minDisBetweenEnemies = 0.5f;
	public Transform groundCheck;
	public LayerMask ground;
	public LayerMask enemy;
	public static float positionX;
	public static int Health = 100;
	public static int Attack=5;
	public static bool beingAttacked = false;
	public static bool beIdle = false;
	public AudioSource punch;

	private bool grounded = false;
	private float facingRight = 1f;
	private Rigidbody2D rb;
	private Animator anim;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		Health = 100;
	}

	void Update(){
		
		if (GameManager.gm.gamestate == GameManager.GameState.Menu) {
			beIdle = true;

		}
		else if (Health <= 0) {
			beIdle = false;
			anim.SetFloat ("health", Health);
			GameManager.gm.gamestate = GameManager.GameState.Lose;
		} else {
			beIdle = false;
			positionX = transform.position.x; //raycast
			if (grounded && Input.GetButtonDown ("Jump")) {
				anim.SetBool ("Ground", false);
				rb.AddForce (new Vector2 (0, jumpForce));
			}

			if (Input.GetButtonDown ("Punch")) {

				punch.Play ();
				checkAttack ();
								
			} else {
				anim.SetBool ("punch", false);
			}
		}
		anim.SetBool ("beIdle", beIdle);	
	
	}
	

	void FixedUpdate () {

		if (GameManager.gm.gamestate == GameManager.GameState.Playing) {
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRaduis, ground);
			anim.SetBool ("Ground", grounded);

			anim.SetFloat ("vSpeed", rb.velocity.y);

			float move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Speed", Mathf.Abs (move));
			rb.velocity = new Vector2 (move * speed, rb.velocity.y);
			if (move > 0 && facingRight==-1f)
				Flip ();
			else if (move < 0 && facingRight==1f)
				Flip ();
			
		}

	}

	void Flip(){
		facingRight *= -1f;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	void checkAttack(){
		anim.SetBool ("punch", true);
		RaycastHit2D hit=Physics2D.Raycast(transform.position,new Vector2(facingRight,0.0f),minDisBetweenEnemies,enemy);
		if (hit.collider != null) {
				GameObject ene = hit.transform.gameObject;
				Enemies enemy = ene.GetComponent<Enemies> ();
				if (enemy.playerPunch == false) {
					enemy.playerPunch = true;

				}
				

		}
			
	}

}

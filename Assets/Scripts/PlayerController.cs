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
	public static bool beIdle = true;
	public static Animator anim;

	public AudioSource punch;

	private bool grounded = false;
	public static float facingRight = 1f;
	private Rigidbody2D rb;



	// Use this for initialization
	void Start () {

		facingRight = 1f;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		Health = 100;

	}

	void Update(){

	 if (GameManager.gm.gamestate == GameManager.GameState.Playing) {

			positionX = transform.position.x;

			beIdle = false;
			anim.SetBool ("beIdle", beIdle);	
			//if player dies
			if (Health <= 0) {
				anim.SetFloat ("health", Health);
				GameManager.gm.gamestate = GameManager.GameState.Lose;
			} 

			//when player is not dead
			else {
				
				//for punching
				if (Input.GetButtonDown ("Punch")) {

					punch.Play ();
					checkAttack (); //checking if the punch hit the enemy
								
				} else {
					anim.SetBool ("punch", false);  
				}
			}

		}	
	}
	

	void FixedUpdate () {

		if (GameManager.gm.gamestate == GameManager.GameState.Playing) {

			//to check if player is on ground or not
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundRaduis, ground);
			anim.SetBool ("Ground", grounded);

			//for jumping
			if (grounded && Input.GetButtonDown ("Jump")) {
				grounded = false;
				anim.SetBool ("Ground", grounded);
				rb.velocity = new Vector2(rb.velocity.x, jumpForce); //using velocity because AddFoce gave inconsistent jumps
			}
			
			anim.SetFloat ("vSpeed", rb.velocity.y); // checking vertical velocity

			//horizontal movement
			float move = Input.GetAxis ("Horizontal");
			anim.SetFloat ("Speed", Mathf.Abs (move));
			rb.velocity = new Vector2 (move * speed, rb.velocity.y);

			//for turning left or right
			if (move > 0 && facingRight==-1f)
				Flip ();
			else if (move < 0 && facingRight==+1f)
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
		RaycastHit2D hit=Physics2D.Raycast(transform.position,new Vector2(facingRight,0.0f),minDisBetweenEnemies,enemy); //I made a raycast so that if it's within the range, the enemies health will be decreased
		if (hit.collider != null) {
				GameObject ene = hit.transform.gameObject;
				Enemies enemy = ene.GetComponent<Enemies> ();
				if (enemy.playerPunch == false) {
					enemy.playerPunch = true;

				}
				

		}
			
	}

	private void OnDrawGizmoSelected(){
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (groundCheck.position, groundRaduis);
	}


}

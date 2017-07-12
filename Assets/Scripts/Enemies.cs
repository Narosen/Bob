using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemies : MonoBehaviour {

	public float playerCheckRaduis = 10.0f;
	public float minDis = 1.0f;
	public float speed = 3.0f;
	public int health = 10;
	public int Damage=15;
	public LayerMask player;
	public bool playerPunch = false;
	public AudioSource hit;
	public AudioSource die;
	public bool facingRight = true;


	public bool isDeath = false;
	protected bool playerSpoted = false;
	protected int flipFlag = 1;
	protected int colTag = 0;
	protected Animator anim;


	// Use this for initialization
	protected void Start () {
		anim = GetComponent<Animator> ();

	}

	protected void Update(){

		if (health > 0) {
			if (facingRight)
				flipFlag = 1;
			else
				flipFlag = -1;

			if (playerSpoted) {
				if (Vector3.Distance (GameManager.gm.player.transform.position, transform.position) > minDis) { 
					transform.position += transform.right * speed * Time.deltaTime * flipFlag;
					anim.SetBool ("touchPlayer", false);
				} else {
					anim.SetBool ("touchPlayer", true);
					hit.Play ();
					if (colTag == 0) {
						PlayerController.beingAttacked = true;
						StartCoroutine (Attack ());

					}
				}
				if (playerPunch == true) {
					StartCoroutine (BeingHit ());
				}
				playerPunch = false;

			}
		} else {
			isDeath = true;
		}

		if (isDeath) {
			death ();
		}
		
	}

	protected void FixedUpdate () {

		playerSpoted = Physics2D.OverlapCircle (transform.position, playerCheckRaduis, player);
		anim.SetBool ("playerSpoted", playerSpoted);

		if (playerSpoted) {
			
			if ( PlayerController.positionX < transform.position.x && facingRight)
				Flip ();
			else if (PlayerController.positionX > transform.position.x && !facingRight)
				Flip ();
		}

	}

	protected void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	protected IEnumerator Attack(){
		colTag = 1;
		PlayerController.Health -= Damage;
		PlayerController.beingAttacked = false;
		yield return new WaitForSeconds(1);
		colTag = 0;

	}

	protected IEnumerator BeingHit(){
		anim.SetBool ("playerPunch", playerPunch);
		health -= PlayerController.Attack;
		yield return new WaitForSeconds(0.1f);
		anim.SetBool ("playerPunch", playerPunch);
	}

	protected void death(){
		die.Play ();
		anim.SetBool ("death", isDeath);
		Destroy (gameObject,0.5f);
	}
}

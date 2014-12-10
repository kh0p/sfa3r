using UnityEngine;
using System.Collections;

public class CharacterControllerScript : MonoBehaviour {

	public float maxSpeed = 10f;
	public float groundRadius = 0.2f; // circle to check if the player is standing on ground
	public Transform groundCheck; 
	public LayerMask whatIsGround; // defining ground
	public float jumpForce = 700f;

	bool facingRight = true;
	bool grounded = false; 
	
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	// In fixed update, there is no need to update the delta of time
	void FixedUpdate () {
		// Checking if we're on the ground
		//grounded = Physics2D.OverlapCricleAll (groundCheck.position, groundRadius, whatIsGround);
		//anim.SetBool ("Ground", grounded);

		//anim.SetFloat ("vSpeed", rigidbody		2D.velocity.y); // velocity of moving up/down

		// Getting the the side from horizontal axis that you're clicking
		float move = Input.GetAxis ("Horizontal");

		// getting absolute value to check if object is moving (direction
		// is not important in this case
		anim.SetFloat("Speed", Mathf.Abs (move));

		rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);
	
		if (move > 0 && !facingRight) {
			Flip ();
		} else if (move < 0 && facingRight) {
			Flip ();
		}
	}

	void OnCollisionEnter (Collision collision) {
		foreach (ContactPoint contact in collision.contacts) {
			Debug.DrawRay (contact.point, contact.normal, Color.white);		
		}
	}

	void Update () {
		if (grounded && Input.GetKeyDown (KeyCode.Space)) {
			anim.SetBool ("Ground", false);
			rigidbody2D.AddForce (new Vector2(0, jumpForce));
		}
	}

	// Flips the sprite
	void Flip () {
		facingRight = !facingRight; // changing state of facingRight
		Vector3 theScale = transform.localScale; // getting local scale 
		theScale.x *= -1; // flipping the x axis
		transform.localScale = theScale; // switching local scale to flipped
	}
}

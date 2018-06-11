using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	// Config
	[SerializeField] float runSpeed = 5f;
	[SerializeField] float jumpSpeed = 5f;
	[SerializeField] float climbSpeed = 5f;

	// State
	bool isAlive = true;

	// Cached component references
	Rigidbody2D myRigidBody2D;
	Animator myAnimator;
	CapsuleCollider2D myBodyCollider2D;
	BoxCollider2D myFeetCollider2D;
	float gravityScaleAtStart;


	// Message then methods
	void Start () {
		myRigidBody2D = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
		myBodyCollider2D = GetComponent<CapsuleCollider2D> ();
		myFeetCollider2D = GetComponent<BoxCollider2D> ();
		gravityScaleAtStart = myRigidBody2D.gravityScale;
	}
	
	// Update is called once per frame
	void Update () {
		Run ();
		Jump ();
		ClimbLadder ();
		FlipSprite ();
	}

	private void Run(){
		float controlThrow = CrossPlatformInputManager.GetAxis ("Horizontal"); 			// from -1 to +1
		Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody2D.velocity.y); // from -5 to +5
		myRigidBody2D.velocity = playerVelocity;

		bool playerHasHorizontalSpeed = Mathf.Abs (myRigidBody2D.velocity.x) > Mathf.Epsilon;
		myAnimator.SetBool ("Running", playerHasHorizontalSpeed);
	}

	private void ClimbLadder(){
		if( ! myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
			myAnimator.SetBool ("Climbing", false);
			myRigidBody2D.gravityScale = gravityScaleAtStart;
			return; 
		}

		float controlThrow = CrossPlatformInputManager.GetAxis ("Vertical");
		Vector2 climbVelocity = new Vector2 (myRigidBody2D.velocity.x, controlThrow * climbSpeed);
		myRigidBody2D.velocity = climbVelocity;
		myRigidBody2D.gravityScale = 0f;

		bool playerHasVerticalSpeed = Mathf.Abs (myRigidBody2D.velocity.y) > Mathf.Epsilon;
		myAnimator.SetBool ("Climbing", playerHasVerticalSpeed );
	}

	private void Jump(){

		if( ! myFeetCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))){ return; }


		if(CrossPlatformInputManager.GetButtonDown("Jump")){
			Vector2 jumpVelocityToAdd = new Vector2 (0f, jumpSpeed);
			myRigidBody2D.velocity += jumpVelocityToAdd;
		}
	}

	private void FlipSprite(){
		bool playerHasHorizontalSpeed = Mathf.Abs (myRigidBody2D.velocity.x) > Mathf.Epsilon; 	// epsilon is instead of 0

		if(playerHasHorizontalSpeed){
			transform.localScale = new Vector2 (Mathf.Sign(myRigidBody2D.velocity.x), 1f); 	// flips the x scale of the sprite

		}
	}
}

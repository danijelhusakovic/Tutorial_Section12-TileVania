﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	// Config
	[SerializeField] float runSpeed = 5f;

	// State
	bool isAlive = true;

	// Cached component references
	Rigidbody2D myRigidBody;
	Animator myAnimator;


	// Message then methods
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
		myAnimator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		Run ();
		FlipSprite ();
	}

	private void Run(){
		float controlThrow = CrossPlatformInputManager.GetAxis ("Horizontal"); 			// from -1 to +1
		Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRigidBody.velocity.y); // from -5 to +5
		myRigidBody.velocity = playerVelocity;

		bool playerHasHorizontalSpeed = Mathf.Abs (myRigidBody.velocity.x) > Mathf.Epsilon;
		myAnimator.SetBool ("Running", playerHasHorizontalSpeed);
	}

	private void FlipSprite(){
		bool playerHasHorizontalSpeed = Mathf.Abs (myRigidBody.velocity.x) > Mathf.Epsilon; 	// epsilon is instead of 0

		if(playerHasHorizontalSpeed){
			transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f); 	// flips the x scale of the sprite

		}
	}
}

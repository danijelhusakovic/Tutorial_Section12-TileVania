﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

	[SerializeField] float runSpeed = 5f;

	Rigidbody2D myRigidBody;

	// Use this for initialization
	void Start () {
		myRigidBody = GetComponent<Rigidbody2D> ();
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
	}

	private void FlipSprite(){
		bool playerHasHorizontalSpeed = Mathf.Abs (myRigidBody.velocity.x) > Mathf.Epsilon; 	// epsilon is instead of 0

		if(playerHasHorizontalSpeed){
			transform.localScale = new Vector2 (Mathf.Sign(myRigidBody.velocity.x), 1f); 	// flips the x scale of the sprite

		}
	}
}

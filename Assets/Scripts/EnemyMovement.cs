﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	[SerializeField] float moveSpeed = 1f;

	Rigidbody2D myRigidBody2D;

	// Use this for initialization
	void Start () {
		myRigidBody2D = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (IsFacingRight ()) {
			myRigidBody2D.velocity = new Vector2 (moveSpeed, 0f);	
		} else {
			myRigidBody2D.velocity = new Vector2 (-moveSpeed, 0f);	
		}
	}

	void OnTriggerExit2D(Collider2D colllision){
		transform.localScale = new Vector2 (-(Mathf.Sign(myRigidBody2D.velocity.x)), 1f);
	}

	bool IsFacingRight () {
		return transform.localScale.x > 0f;
	}

}

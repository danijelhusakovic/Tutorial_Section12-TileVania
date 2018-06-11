using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour {

	[SerializeField] AudioClip coinPickUpSFX;
	[SerializeField] int coinValue = 10;

	private void OnTriggerEnter2D(){
		AudioSource.PlayClipAtPoint (coinPickUpSFX, Camera.main.transform.position);
		FindObjectOfType<GameSession> ().AddToScore(coinValue);
		Destroy (gameObject);
	}

}

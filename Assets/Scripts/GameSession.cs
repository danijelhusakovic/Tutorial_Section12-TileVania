﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour {

	[SerializeField] int playerLives = 3;
	[SerializeField] int score = 0;

	[SerializeField] Text livesText;
	[SerializeField] Text scoreText;

	private void Awake(){
		// Singleton pattern
		int numGameSessions = FindObjectsOfType<GameSession> ().Length;
		if (numGameSessions > 1) {
			Destroy (gameObject);
		} else {
			DontDestroyOnLoad (gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		livesText.text = playerLives.ToString ();
		scoreText.text = score.ToString ();
	}

	public void AddToScore(int pointsToAdd){
		score += pointsToAdd;
		scoreText.text = score.ToString ();
	}

	public void ProcessPlayerDeath(){
		if (playerLives > 1) {
			TakeLife ();
		} else {
			ResetGameSession ();
		}
	}

	private void TakeLife(){
		playerLives--;
		livesText.text = playerLives.ToString ();
		int currentSceneIndex = SceneManager.GetActiveScene ().buildIndex;
		SceneManager.LoadScene (currentSceneIndex);
	}

	private void ResetGameSession () {
		SceneManager.LoadScene (0);
		Destroy (gameObject);
	}

}

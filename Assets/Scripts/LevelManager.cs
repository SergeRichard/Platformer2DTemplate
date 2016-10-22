﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float waitToRespawn;
	public PlayerController thePlayer;
	public GameObject deathExplosion;
	public int coinCount;
	public Text coinText;

	public Image heart1;
	public Image heart2;
	public Image heart3;

	public Sprite heartFull;
	public Sprite heartHalf;
	public Sprite heartEmpty;

	public int maxHealth;
	public int healthCount;

	private bool respawning;

	private ResetOnRespawn[] objectsToReset;

	public bool invincible;

	public Text livesText;

	public int startingLives;
	public int currentLives;

	public GameObject gameOverScreen;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();

		coinText.text = "Coins: " + coinCount;

		healthCount = maxHealth;

		objectsToReset = FindObjectsOfType<ResetOnRespawn> ();

		currentLives = startingLives;
		livesText.text = "Lives x " + currentLives;

	}
	
	// Update is called once per frame
	void Update () {
		if (healthCount <= 0 && !respawning) {
			Respawn ();
			respawning = true;
		}
	}

	public void Respawn()
	{
		currentLives--;
		livesText.text = "Lives x " + currentLives;
		if (currentLives > 0) {
			StartCoroutine (RespawnCo ());	
		} else {
			thePlayer.gameObject.SetActive (false);
			gameOverScreen.SetActive (true);
		}
	}

	public IEnumerator RespawnCo() {
		thePlayer.gameObject.SetActive (false);

		Instantiate (deathExplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		yield return new WaitForSeconds (waitToRespawn);

		healthCount = maxHealth;
		respawning = false;
		UpdateHealthMeter ();

		coinCount = 0;
		coinText.text = "Coins: " + coinCount;

		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive (true);

		for (int i = 0; i < objectsToReset.Length; ++i) {		
			objectsToReset [i].gameObject.SetActive (true);	
			objectsToReset [i].ResetObject ();
		}
	}
	public void AddCoins(int coinsToAdd) {
		coinCount += coinsToAdd;
		coinText.text = "Coins: " + coinCount;
	}

	public void HurtPlayer(int damageToTake) {
		if (!invincible) {
			healthCount -= damageToTake;
			UpdateHealthMeter ();

			thePlayer.KnockBack ();
		}
	}
	public void UpdateHealthMeter () {
		switch (healthCount) {
		case 6:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartFull;
			return;
		case 5:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartHalf;
			return;
		case 4:
			heart1.sprite = heartFull;
			heart2.sprite = heartFull;
			heart3.sprite = heartEmpty;
			return;
		case 3:
			heart1.sprite = heartFull;
			heart2.sprite = heartHalf;
			heart3.sprite = heartEmpty;
			return;
		case 2:
			heart1.sprite = heartFull;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		case 1:
			heart1.sprite = heartHalf;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		default:
			heart1.sprite = heartEmpty;
			heart2.sprite = heartEmpty;
			heart3.sprite = heartEmpty;
			return;
		}

	}
	public void AddLives(int livesToAdd) {
		currentLives += livesToAdd;

		livesText.text = "Lives x " + currentLives;

	}
}

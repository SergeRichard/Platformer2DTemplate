﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float waitToRespawn;
	public PlayerController thePlayer;
	public GameObject deathExplosion;
	public int coinCount;
	public Text coinText;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();

		coinText.text = "Coins: " + coinCount;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Respawn()
	{
		StartCoroutine (RespawnCo ());	
	}

	public IEnumerator RespawnCo() {
		thePlayer.gameObject.SetActive (false);

		Instantiate (deathExplosion, thePlayer.transform.position, thePlayer.transform.rotation);

		yield return new WaitForSeconds (waitToRespawn);

		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive (true);
	}
	public void AddCoins(int coinsToAdd) {
		coinCount += coinsToAdd;
		coinText.text = "Coins: " + coinCount;
	}
}

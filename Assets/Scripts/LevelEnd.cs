﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {

	public string levelToLoad;

	private PlayerController thePlayer;
	private CameraController theCamera;
	private LevelManager theLevelManager;
	private bool movePlayer;

	public float waitToMove;
	public float waitToLoad;

	public Sprite flagOpen;

	private SpriteRenderer theSpriteRenderer;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();
		theCamera = FindObjectOfType<CameraController> ();
		theLevelManager = FindObjectOfType<LevelManager> ();
		theSpriteRenderer = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (movePlayer) {
			thePlayer.myRigidbody.velocity = new Vector3 (thePlayer.moveSpeed, thePlayer.myRigidbody.velocity.y, 0f);
		}
	}
	void OnTriggerEnter2D(Collider2D other) {

		if (other.tag == "Player") {

			theSpriteRenderer.sprite = flagOpen;

			StartCoroutine(LevelEndCo());
		}

	}
	public IEnumerator LevelEndCo() {
		thePlayer.canMove = false;
		theCamera.followTarget = false;
		theLevelManager.invincible = true;

		theLevelManager.levelMusic.Stop ();
		theLevelManager.gameOverMusic.Play ();

		thePlayer.myRigidbody.velocity = Vector3.zero;

		yield return new WaitForSeconds (waitToMove);

		movePlayer = true;

		yield return new WaitForSeconds (waitToLoad);

		SceneManager.LoadScene (levelToLoad);
	}
}

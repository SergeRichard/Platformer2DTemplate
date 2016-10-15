using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public float waitToRespawn;
	public PlayerController thePlayer;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Respawn()
	{
		thePlayer.gameObject.SetActive (false);
		thePlayer.transform.position = thePlayer.respawnPosition;
		thePlayer.gameObject.SetActive (true);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Variables conected to external game objects 
	public GameObject game;
	public GameObject enemyGenerator; // Creates the enemies from a prefab 
	public AudioClip jumpClip; // Each clip is for a specific sound 
	public AudioClip dieClip;
	public AudioClip pointClip;
	public ParticleSystem dust; // This particle is the dust of the ground 

	private Animator animatorComponent; // Controls the sprite animations 
	private AudioSource audioPlayer; // Reproduces the clips 

	private float initialY; // To check double jump when no grounded 
	private bool gamePlaying; // State of the game 
	private bool isGrounded; // If the player is on the ground 
	private bool userAction; // Checks for the user keys inputs

	// Use this for initialization
	void Start () {
		// Get the componenets owned by the player and its y position 
		animatorComponent = GetComponent<Animator>();
		audioPlayer = GetComponent<AudioSource>();
		initialY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		// This 3 conditions should be meet before a jump 
		isGrounded = this.transform.position.y == initialY;
		gamePlaying = game.GetComponent<GameController>().actualGameState == GameController.GameState.Playing;
		userAction = (Input.GetKeyDown ("space") || Input.GetMouseButtonDown (0));

		if (isGrounded && gamePlaying && userAction) {
			UpdateState("PlayerJump");
			audioPlayer.clip = jumpClip;
			audioPlayer.Play();
		}
	}

	// Changes the animation of the player using states 
	public void UpdateState(string state = null) {
		if( state != null) {
			animatorComponent.Play(state);
		}
	}

	// This function is called whenever is a 2d collision with the player 
	void OnTriggerEnter2D(Collider2D other){
		// Tags are used to check whose the enemy 
		if (other.gameObject.tag == "Enemy") {
			// Logic changes 
			UpdateState("PlayerDie");
			game.GetComponent<GameController>().actualGameState = GameController.GameState.Ended;
			enemyGenerator.SendMessage("CancelGenerator", true);
			game.SendMessage("ResetTimeScale", 0.5f);

			// Audio changes 
			game.GetComponent<AudioSource>().Stop();
			audioPlayer.clip = dieClip;
			audioPlayer.Play();

			// Desactivate the particle system 
			DustStop();
		} else if (other.gameObject.tag == "Point") {
			game.SendMessage("IncreasePoints");
			audioPlayer.clip = pointClip;
			audioPlayer.Play();
		}
	}

	// To prevent game restart too fast at clicking
	void GameReady() {
		game.GetComponent<GameController>().actualGameState = GameController.GameState.Ready;
	}

	// Activates the particle system animation 
	void DustPlay() {
		dust.Play();
	}

	// Desactivate the particle system animation 
	void DustStop() {
		dust.Stop();
	}
}

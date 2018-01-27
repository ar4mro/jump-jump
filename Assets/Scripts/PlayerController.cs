using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject game;
	public GameObject enemyGenerator; 
	public AudioClip jumpClip;
	public AudioClip dieClip;

	private Animator animatorComponent;
	private AudioSource audioPlayer; 
	private float initialY;

	private bool gamePlaying; 
	private bool isGrounded; 
	private bool userAction; 

	// Use this for initialization
	void Start () {
		animatorComponent = GetComponent<Animator>();
		audioPlayer = GetComponent<AudioSource>();
		initialY = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
		isGrounded = this.transform.position.y == initialY;
		gamePlaying = game.GetComponent<GameController>().actualGameState == GameController.GameState.Playing;
		userAction = (Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0));

		if (isGrounded && gamePlaying && userAction) {
			UpdateState("PlayerJump");
			audioPlayer.clip = jumpClip;
			audioPlayer.Play();
		}
	}

	public void UpdateState(string state = null) {
		if( state != null) {
			animatorComponent.Play(state);
		}
	}

	// This function is called whenever is a 2d collision 
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Enemy") {
			UpdateState("PlayerDie");
			game.GetComponent<GameController>().actualGameState = GameController.GameState.Ended;
			enemyGenerator.SendMessage("CancelGenerator", true);

			game.GetComponent<AudioSource>().Stop();
			audioPlayer.clip = dieClip;
			audioPlayer.Play();
		}
	}

	// To prevent game restart too fast at clicking
	void GameReady() {
		game.GetComponent<GameController>().actualGameState = GameController.GameState.Ready;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject game;
	public GameObject enemyGenerator; 

	private Animator animatorComponent;
	private bool gamePlaying; 

	// Use this for initialization
	void Start () {
		animatorComponent = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		gamePlaying = game.GetComponent<GameController>().actualGameState == GameController.GameState.Playing;
		if (gamePlaying && (Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0))) {
			UpdateState("PlayerJump");
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
		}
	}

	// To prevent game restart too fast at clicking
	void GameReady() {
		game.GetComponent<GameController>().actualGameState = GameController.GameState.Ready;
	}
}

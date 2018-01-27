using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	// GAME WORLD 
	[Range (0f, 0.20f)] // Limits the value in te UI 
	public float parallaxSpeed = 0.02f;
	public RawImage background;
	public RawImage platform; 
	public GameObject uiMenu; 

	// GAME STATES
	public enum GameState {Idle, Playing, Ended, Ready};
	public GameState actualGameState = GameState.Idle;

	public GameObject player; 
	public GameObject enemyGenerator; 

	private AudioSource musicPlayer;

	private float finalSpeed; 

	// Use this for initialization
	void Start () {
		musicPlayer = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

		bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0) ;

		// When the game starts 
		if (actualGameState == GameState.Idle && userAction) {
			actualGameState = GameState.Playing;

			// Hides the menu and desactives all its elements 
			uiMenu.SetActive(false);
			player.SendMessage("UpdateState", "PlayerRun");
			enemyGenerator.SendMessage("StartGenerator");
			musicPlayer.Play();
		} else if (actualGameState == GameState.Playing) {
			Parallax ();
		} else if (actualGameState == GameState.Ready) {
			if (userAction) {
				RestartGame();
			}
		}
	}

	// Performs the background and terrain effect 
	void Parallax() {
		// Delta time multiplication is needed due different resolutions 
		finalSpeed = parallaxSpeed * Time.deltaTime;

		// Updates the velocity of the parallax objects
		background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
		platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
	}

	public void RestartGame() {
		SceneManager.LoadScene("Main"); 
	}
}

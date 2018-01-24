using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// GAME WORLD 
	[Range (0f, 0.20f)] // Limits the value in te UI 
	public float parallaxSpeed = 0.02f;
	public RawImage background;
	public RawImage platform; 
	public GameObject uiMenu; 

	// GAME STATES
	public enum GameState {Idle, Playing};
	public GameState actualGameState = GameState.Idle;

	public GameObject player; 

	private float finalSpeed; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		// When the game starts 
		if (actualGameState == GameState.Idle && (Input.GetKeyDown("up") || Input.GetMouseButtonDown(0))) {
			actualGameState = GameState.Playing;

			// Hides the menu and desactives all its elements 
			uiMenu.SetActive(false);
			player.SendMessage("UpdateState", "PlayerRun");
		} else if (actualGameState == GameState.Playing) {
			Parallax ();
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// This class controls all the mechanics and interactions in this game 
public class GameController : MonoBehaviour {

	// GAME WORLD 
	[Range (0f, 0.20f)] // Limits the value in te UI of the next variable 
	public float parallaxSpeed = 0.02f; // The speed of the parallax effect
	public RawImage background;
	public RawImage platform; 
	public GameObject uiMenu; 
	public GameObject uiScore;
	public GameObject uiFinal;
	public Text pointsText; 
	public Text maxScore;
	public Text scoreResult; 

	// GAME STATES
	public enum GameState {Idle, Playing, Ended, Ready};
	public GameState actualGameState = GameState.Idle;

	// GAME OBJECTS
	public GameObject player; // Player 
	public GameObject enemyGenerator; // Enemies 

	// PUBLIC VARIABLES 
	public float scaleTime = 6f; // Every n seconds 
	public float scaleIncrement = .25f;

	// LOCAL VARIABLES 
	private AudioSource musicPlayer; 
	private int points = 0; // Score
	private float finalSpeed; 

	// Use this for initialization
	void Start () {
		musicPlayer = GetComponent<AudioSource>();
		maxScore.text = "BEST: " + GetMaxScore().ToString();
	}
	
	// Update is called once per frame
	void Update () {

		// Checks for user input 
		bool userAction = Input.GetKeyDown("space") || Input.GetMouseButtonDown(0) ;

		// When the game starts 
		if (actualGameState == GameState.Idle && userAction) {
			actualGameState = GameState.Playing;

			// Hides the menu and desactives all its elements 
			uiMenu.SetActive(false);
			uiScore.SetActive(true);

			player.SendMessage("UpdateState", "PlayerRun");
			player.SendMessage("DustPlay");
			enemyGenerator.SendMessage("StartGenerator");

			musicPlayer.Play();

			// To increase difficulty
			InvokeRepeating("GameTimeScale", scaleTime, scaleTime);
		} else if (actualGameState == GameState.Playing) {
			Parallax();
		} else if (actualGameState == GameState.Ready) {
			uiScore.SetActive(false);
			uiFinal.SetActive(true);
			scoreResult.text = "Your Score: " + points.ToString();
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

	// Restarts the game 
	public void RestartGame() {
		ResetTimeScale();
		SceneManager.LoadScene("Main"); 
	}

	// Changes the scale the time is passing, 1 is as fast as realtime, 0.5
	// would be like slow motion, gives the illusion of difficulty 
	void GameTimeScale() {
		Time.timeScale += scaleIncrement;
	}

	// Reset the scale of the time 
	public void ResetTimeScale(float newTimeScale = 1f) {
		CancelInvoke("GameTimeScale");
		Time.timeScale = newTimeScale; 
		// Debug.Log("Scale time reset " + Time.timeScale.ToString());
	}

	// Updates the obtained points 
	public void IncreasePoints() {
		points++;
		pointsText.text = points.ToString();
		if(points >= GetMaxScore()) {
			maxScore.text = "BEST: " + points.ToString();
			SaveMaxScore(points);
		}
	}

	// Gets the actual maximum score 
	public int GetMaxScore() {
		return PlayerPrefs.GetInt("Max Points", 0);
	}

	// Saves the new score 
	public void SaveMaxScore(int currentPoints) {
		PlayerPrefs.SetInt("Max Points", currentPoints);
	}
}

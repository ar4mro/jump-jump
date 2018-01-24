using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	// Limits the value in te UI 
	[Range (0f, 0.20f)]
	public float parallaxSpeed = 0.02f;

	public RawImage background;
	public RawImage platform; 

	private float finalSpeed; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// Delta time multiplication is needed due different resolutions 
		finalSpeed = parallaxSpeed * Time.deltaTime;

		// Updates the velocity of the parallax object 
		background.uvRect = new Rect(background.uvRect.x + finalSpeed, 0f, 1f, 1f);
		platform.uvRect = new Rect(platform.uvRect.x + finalSpeed * 4, 0f, 1f, 1f);
	}
}

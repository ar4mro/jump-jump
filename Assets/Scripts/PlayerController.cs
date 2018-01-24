using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Animator animatorComponent;

	// Use this for initialization
	void Start () {
		animatorComponent = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("up") || Input.GetMouseButtonDown (0)) {
			UpdateState("PlayerJump");
		}
	}

	public void UpdateState(string state = null) {
		if( state != null) {
			animatorComponent.Play(state);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float velocity = 2f;

	private Rigidbody2D rb2d; // A boby must exists to enable collides 

	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D>();
		rb2d.velocity = Vector2.left * velocity;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Whenever and object collides with this object
	void OnTriggerEnter2D(Collider2D other){
		// Tags are used to identify which object 
		if (other.gameObject.tag == "Destroyer") {
			Destroy(gameObject);
		}
	}
}

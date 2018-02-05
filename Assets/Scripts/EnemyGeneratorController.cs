using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is a prefab, a base to create oher objects of this type
// and keep its behaivor and properties 
public class EnemyGeneratorController : MonoBehaviour {
	
	public GameObject enemyPrefab; // The actual object 
	public float generatorTimer = 1.75f; // The velocity the objects will be created 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void CreateEnemy() {
		// Instantiate takes the game object, the position, and some thing of particles 
		Instantiate(enemyPrefab, this.transform.position, Quaternion.identity);
	}

	public void StartGenerator() {
		// This method will invoke the method in n seconds with a period of generatoTimer 
		InvokeRepeating("CreateEnemy", 0f, generatorTimer);
	}
		
	public void CancelGenerator(bool clean = false) {
		// Cancels the repeating 
		CancelInvoke("CreateEnemy");

		if (clean) {
			// Destoys all the created objets to start the game again 
			Object[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
			foreach (GameObject enemy in allEnemies) {
				Destroy(enemy);
			}
		}
	}
}

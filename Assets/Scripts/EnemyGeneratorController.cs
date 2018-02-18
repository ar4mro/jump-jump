using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is a prefab, a base to create oher objects of this type
// and keep its behaivor and properties 
public class EnemyGeneratorController : MonoBehaviour {
	
	public GameObject enemyPrefab; // The actual object 
	public float generatorTimer = 1.75f; // The velocity between the objects will be created 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void CreateEnemy() {
		float randomValue; 

		// Instantiate takes the game object, the position, and some thing of particles 
		GameObject enemyObject = Instantiate(enemyPrefab, this.transform.position, Quaternion.identity) as GameObject;

		randomValue = Random.value;
		// Change its local scale in x y z format depending of the probability 
		if (randomValue  > 0.7 && randomValue <= 0.9) {
			enemyObject.transform.localScale = new Vector3(1.45f, 1.45f, 1); 
		} else if (randomValue > 0.9 && randomValue <= 0.99 ) {
			enemyObject.transform.localScale = new Vector3(1.75f, 1.75f, 1); 
		} else if (randomValue > 0.99) { 
			enemyObject.transform.localScale = new Vector3(2, 2, 1); 
		}
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

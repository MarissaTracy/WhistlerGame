using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		EnemyManager enemyManager = GameObject.Find("Spawner").GetComponent<EnemyManager>();
		if (enemyManager.numEnemies <= 0){
			LoadNextLevel();
		}
	}

	public void SpawnEnemies(){
		EnemyManager enemyManager = GameObject.Find("Spawner").GetComponent<EnemyManager>();
		enemyManager.CreateEnemies();
	}

	public void LoadNextLevel(){
		EnemyManager enemyManager = GameObject.Find("Spawner").GetComponent<EnemyManager>();
		enemyManager.CreateEnemies();
		// Maybe add stuff later
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave {
	
	private int spawnCount; 
	private float delayPerSpawn;
	private float length;
	private GameObject[] enemies;



	public Wave(int spawnCount, GameObject enemy, float length,float delayPerSpawn) {
		this.spawnCount = spawnCount;
		enemies = new GameObject[spawnCount];
		this.length = length;
		this.delayPerSpawn = delayPerSpawn;
		setEnemies (enemy);
	}

	public void setSpawnCount(int spawnCount) {
		this.spawnCount = spawnCount;
	}

	public void setEnemies(GameObject enemy) {
		for (int i = 0; i < spawnCount; i++) {
			enemies [i] = enemy;
		}
	}
	public GameObject[] getEnemies() {
		return enemies;
	}

	public int getSpawnCount() {
		return this.spawnCount;
	}
		
	public GameObject spawnEnemy() {
		return this.enemies [--spawnCount];
	}

	public float getDelayPerSpawn() {
		return this.delayPerSpawn;
	}

	public float waveLength() {
		return this.length;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator {

	private int level;
	private GameObject enemy;
	private Wave wave;
	private float delay;


	public WaveGenerator(GameObject enemy) {
		this.level = 1;
		this.enemy = enemy;
		this.delay = 1.5f;
		generate ();

	}

	public void setLevel(int level) {
		this.level = level;
	}

	public void generate() {
		wave = new Wave (level * 5, enemy,delay*level*5+10,delay);
	}
	public int getSpawnCount() {
		return wave.getSpawnCount ();
	}

	public float getDelayPerSpawn() {
		return wave.getDelayPerSpawn ();
	}

	public GameObject spawnEnemy() {
		return wave.spawnEnemy ();
	}
	public float waveLength() {
		return wave.waveLength ();
	}

	public GameObject[] getEnemies() {
		return wave.getEnemies ();
	}

	public void levelUp(GameObject enemy) {
		this.level += 1;
		if(this.delay >= 0.5f)
			this.delay -= 0.2f;
		this.enemy = enemy;
	}


}

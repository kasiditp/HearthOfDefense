using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

	public int lives = 5;
	public int money = 100;
	public int score = 0;
	public int level = 1;
	public int stage = 1;

	public Text moneyText;
	public Text livesText;
	public Text timeText;
	public Text scoreText;
	public Text stageText;
	public Text levelText;

	public GameObject gameOverScreen;

	//Suggest (Trong) : Enemy may have attack point and lose life depend on attack point instead
	public void LoseLife(int l = 1) {
		lives -= l;
		if(lives <= 0) {
			GameOver();
		}
	}
	public void GameOver() {
		Debug.Log("Game Over");
		// TODO: Send the player to a game-over screen instead!
		gameOverScreen.SetActive(true);
		Time.timeScale = 0;
		//SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
	public void loadMainMenu() {
		SceneManager.LoadScene("MainMenu");
	}

	public void LevelUp() {
		level++;
	}

	public void ChangeStage() {
		level = 1;
		stage++;
	}

	void Update() {
		// FIXME: This doesn't actually need to update the text every frame.
		moneyText.text =  money.ToString();
		livesText.text = lives.ToString();
		timeText.text = "Next wave : " + SpawnerController.TimeLeft().ToString();
		scoreText.text = score.ToString ();
		stageText.text = "Stage: " + stage.ToString ();
		levelText.text = "Level: " + level.ToString ();
	}

}

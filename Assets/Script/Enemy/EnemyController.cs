using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour 
{
	private Enemy enemy;

	public float speed;
	public float health;
	public int money;
	public int attack;
	public int score;
	public bool dead;
	public AudioSource audioSource;
	public GameObject healthBar;
	public GameObject deadEffect;
	ScoreManager sm;


	void Start () 
	{
		sm = FindObjectOfType<ScoreManager> ();
		int round = sm.stage / 5 + 1;

		dead = false;
		if (this.name.Equals ("simpleenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
			enemy = new SimpleEnemy ("Simple",this.speed, this.health + (sm.level*2f * round), this.money+sm.level*sm.stage, this.attack, this.score);
		} else if (this.name.Equals ("advanceenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
			enemy = new AdvanceEnemy ("Advance",this.speed, this.health +(sm.level*4f * round), this.money+sm.level*sm.stage, this.attack, this.score);
		} 
		else if (this.name.Equals ("specialenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
			enemy = new SpecialEnemy ("Special",this.speed, this.health + (sm.level*6f * round) , this.money+sm.level*sm.stage, this.attack, this.score);
		} 
		else if (this.name.Equals ("epicenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
			enemy = new EpicEnemy ("Epic",this.speed, this.health + (sm.level*8f * round), this.money+sm.level*sm.stage, this.attack, this.score);
		}
		else if (this.name.Equals ("legendaryenemy(clone)", System.StringComparison.InvariantCultureIgnoreCase)) {
			enemy = new LegendaryEnemy ("Legendary",this.speed, this.health + (sm.level*10f *round), this.money+sm.level*sm.stage, this.attack, this.score);
		}
		else {
			enemy = new SimpleEnemy ("Simple",this.speed, this.health + (sm.level*2f * round), this.money+sm.level*sm.stage , this.attack, this.score);
		}
		audioSource = FindObjectOfType<AudioSource> ();
		float percent = enemy.getHealth()/this.health;
		float realValue = percent * 1.761128f;

		healthBar.transform.localScale = new Vector3 (healthBar.transform.localScale.x, healthBar.transform.localScale.y, realValue);


		//Let's illustrate inheritance with the 
		//default constructors.

	}

	bool IsDead() {
		return enemy.getHealth() <= 0;
	}

	void increaseMoneyAndScore() {
		sm.money += enemy.getMoney ();
		sm.score += enemy.getScore ();
	}

	void destroy() {
		if (audioSource == null) {
			GameObject dead = Instantiate (deadEffect, new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z), transform.rotation);
			transform.position = Vector3.one * -9999999f;
			Destroy (this.gameObject);
			Destroy (dead,2f);
		}
		else {
			GameObject dead = Instantiate (deadEffect, new Vector3(transform.position.x,transform.position.y + 0.5f,transform.position.z), transform.rotation);
			audioSource.Play ();
			transform.position = Vector3.one * -9999999f;
			Destroy (this.gameObject,audioSource.clip.length);
			Destroy (dead,2f);
		}

	}
		
	void Update() {
		
	}
		

	public void TakeDamage(float damage){
		enemy.decreaseHealth(damage);

		if (IsDead () && !dead) {
			dead = true;
			increaseMoneyAndScore ();
			destroy();
	
		}
		float percent = enemy.getHealth()/this.health;
		float realValue = percent * 1.761128f;

		healthBar.transform.localScale = new Vector3 (healthBar.transform.localScale.x, healthBar.transform.localScale.y, realValue);



	}

	void FixedUpdate() {
		
	}


		
}
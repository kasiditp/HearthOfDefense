using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Tower : MonoBehaviour {

	Transform turretTransform;

	public float range = 10f;
	public GameObject bulletPrefab;

	public int cost = 5;
	public int sellCost;

	float fireCooldown = 0.5f;
	float fireCooldownLeft = 0;

	public float damage = 5f;
	public float radius = 0;

	// public canvas uiUpgrade;
	public GameObject tempUI;
	// Use this for initialization
	void Start () {
		turretTransform = transform.Find("Turret");
		sellCost = (int)System.Math.Round(0.8*cost);
		// sellCost = 
	}
	
	// Update is called once per frame
	void Update () {
		// TODO: Optimize this!
		EnemyController[] enemies = GameObject.FindObjectsOfType<EnemyController>();

		EnemyController nearestEnemy = null;
		float dist = Mathf.Infinity;

		foreach(EnemyController e in enemies) {
			float d = Vector3.Distance(this.transform.position, e.transform.position);
			if(nearestEnemy == null || d < dist) {
				nearestEnemy = e;
				dist = d;
			}
		}

		if(nearestEnemy == null) {
//			Debug.Log("No enemies?");
			return;
		}

		Vector3 dir = nearestEnemy.transform.position - this.transform.position;

		Quaternion lookRot = Quaternion.LookRotation( dir );

		//Debug.Log(lookRot.eulerAngles.y);
		turretTransform.rotation = Quaternion.Euler( -90, lookRot.eulerAngles.y, 0 );

		fireCooldownLeft -= Time.deltaTime;
		if(fireCooldownLeft <= 0 && dir.magnitude <= range) {
			fireCooldownLeft = fireCooldown;
			ShootAt(nearestEnemy);
		}

	}
	void OnMouseEnter() {
		// Debug.Log("Hover tower");
		// GameObject germSpawned = Instantiate(uiUpgrade) as GameObject;
 		// germSpawned.transform.SetParent(canvas.transform);
 		// germSpawned.transform.localPosition = spawnPosition;
 		// germSpawned.transform.localRotation = spawnRotation;
		// Renderer blockRender = blockModel.GetComponent<Renderer>();
		// blockRender.material.color = Color.blue;

	}
	/// <summary>
	/// Called when the mouse is not any longer over the GUIElement or Collider.
	/// </summary>
	void OnMouseExit() {
		// Renderer blockRender = blockModel.GetComponent<Renderer>();
		// blockRender.material.color = originalColor;
		Destroy(tempUI);
	}

	void ShootAt(EnemyController e) {
		// TODO: Fire out the tip!
		GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, this.transform.position, this.transform.rotation);

		Bullet b = bulletGO.GetComponent<Bullet>();
		b.target = e.transform;
		b.damage = damage;
		b.radius = radius;
	}

	public void SellTower(){
		Destroy(this.gameObject);
		// isCreatTower
		GameObject.FindObjectOfType<GroundTileControl>().isCreatTower = false;
	}

	public void UpgradeTower(){
		cost = cost*2;
		damage = damage*2;
		sellCost = (int)System.Math.Round(0.8*cost);
	}
}

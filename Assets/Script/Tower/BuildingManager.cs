using UnityEngine;
using System.Collections;

public class BuildingManager : MonoBehaviour {
	public GameObject selectedTower;
	public GameObject selectedDummyTower;
	public GameObject pathChecker;
	private GameObject tempDummyTower;

	// Use this for initialization
	void Start () {
		tempDummyTower = Instantiate(selectedDummyTower, new Vector3(0,0,0), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void SelectTowerType(GameObject prefab) {
		selectedTower = prefab;
		
	}
	public void SelectDummyTowerType(GameObject dummyTower){
		selectedDummyTower = dummyTower;
		// if(tempDummyTower = null){
		// 	tempDummyTower = Instantiate(selectedDummyTower, new Vector3(0,0,0), Quaternion.identity);
		// } else {
			Destroy(tempDummyTower);
			tempDummyTower = Instantiate(selectedDummyTower, new Vector3(0,0,0), Quaternion.identity);
		// }
	}
	public void rescanPath() {
		AstarPath.active.Scan();
	}
	public void recalculatePath() {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		foreach(GameObject enemy in enemies) {
			enemy.GetComponent<Pather>().calculatePath();
		}
	}
	public void SellBuilding(Tower tower) {
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
		sm.money += tower.sellCost;
		tower.SellTower();
	}
	public void UpgradeBuilding(Tower tower){
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
		if(sm.money < tower.cost) {
			Debug.Log("Not enough money!");
			return;
		}
		sm.money -= tower.cost;
		tower.UpgradeTower();
	}
	public void SetTowerInformation(GameObject tower){
		Debug.Log("KKKKKKKKKKKKKKKKKKKKKK");
		TowerInfoCanvas canvas = tower.GetComponent<TowerInfoCanvas>();
		canvas.FadeText();
	}
	public GameObject getDummyTower(){
		return this.tempDummyTower;
	}
}

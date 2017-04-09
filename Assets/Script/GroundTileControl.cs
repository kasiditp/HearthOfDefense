using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class GroundTileControl : MonoBehaviour {
	GameObject blockModel;
	Color originalColor;
	BuildingManager bm;
	float boxHeight = 2.4f;
	float blockSizeX;
	float blockSizeY;
	float blockSizeZ;
	GameObject tempTower;
	public bool isCreatTower;
	// Use this for initialization
	void Start () {
		blockModel = this.gameObject;
		originalColor = blockModel.GetComponent<Renderer>().material.color;
		bm = GameObject.FindObjectOfType<BuildingManager>();

		blockSizeX = blockModel.GetComponent<Renderer>().bounds.size.x;
		blockSizeY = blockModel.GetComponent<Renderer>().bounds.size.y;
		blockSizeZ = blockModel.GetComponent<Renderer>().bounds.size.z;
		isCreatTower = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	/// <summary>
	/// Called when the mouse enters the GUIElement or Collider.
	/// </summary>
	void OnMouseEnter() {
		
		// Renderer blockRender = blockModel.GetComponent<Renderer>();
		// blockRender.material.color = Color.blue;
		// tempTower = Instantiate(bm.selectedDummyTower,  new Vector3(transform.parent.position.x - blockSizeX/2, transform.parent.position.y + blockSizeY, transform.parent.position.z - blockSizeZ/2), transform.parent.rotation);
		// isCreatDummy = true;
		// Debug.Log(isCreatTower);
		// && !isCreatTower
		if(bm.getDummyTower() != null ){
			bm.getDummyTower().transform.position = new Vector3(transform.parent.position.x - blockSizeX/2, transform.parent.position.y + blockSizeY, transform.parent.position.z - blockSizeZ/2);
		} else {
			bm.getDummyTower().SetActive(false);
		}
	}
	/// <summary>
	/// Called when the mouse is not any longer over the GUIElement or Collider.
	/// </summary>
	void OnMouseExit() {
		Renderer blockRender = blockModel.GetComponent<Renderer>();
		blockRender.material.color = originalColor;
		bm.getDummyTower().SetActive(true);
	}
    /// <summary>
	/// OnMouseUp is called when the user has released the mouse button.
	/// </summary>
	void OnMouseUp()
	{
		bm.rescanPath();
		if(bm.selectedTower != null) {
			ScoreManager sm = GameObject.FindObjectOfType<ScoreManager>();
			if(sm.money < bm.selectedTower.GetComponent<Tower>().cost) {
				Debug.Log("Not enough money!");
				return;
			}
			isCreatTower = true;
			sm.money -= bm.selectedTower.GetComponent<Tower>().cost;

			// FIXME: Right now we assume that we're an object nested in a parent.
			GameObject placedTower = Instantiate(bm.selectedTower,  new Vector3(transform.parent.position.x - blockSizeX/2, transform.parent.position.y + blockSizeY, transform.parent.position.z - blockSizeZ/2), transform.parent.rotation);
			checkPath(placedTower);
			// if (!checkPath(placedTower)) {
			// 	Destroy(placedTower);
			// 	sm.money += bm.selectedTower.GetComponent<Tower>().cost;
			// }
			// Destroy(transform.parent.gameObject);
		}
		bm.rescanPath();
		bm.recalculatePath();
	}
	void checkPath(GameObject placedTower) {
		Debug.Log("Checking path block...");
		BuildingManager bm = GameObject.FindObjectOfType<BuildingManager>();
		GameObject pathcheck = Instantiate(bm.pathChecker, GameObject.Find("START").transform.position, Quaternion.identity);
		pathcheck.GetComponent<PathcheckerControl>().towerToCheck = placedTower;

		// Path path = pathcheck.GetComponent<PathcheckerControl>().path;
		// Vector3 lastNodePos = path.vectorPath[path.vectorPath.Count - 1];
		// if(Vector3.Distance(GameObject.Find("END").transform.position, lastNodePos) > 1) {
		// 	Debug.Log("PATH BLOCKED");
		// 	return false;
		// }
		// else return true;
	}
	
}

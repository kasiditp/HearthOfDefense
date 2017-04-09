using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointScript : MonoBehaviour {

	private AudioSource audioSource;
	// Use this for initialization
	void Start () {
		audioSource = FindObjectOfType<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col) {
		audioSource.Play ();
		Destroy (col.gameObject);
		ScoreManager sm = GameObject.FindObjectOfType<ScoreManager> ();
		sm.LoseLife ();
	
	}
}

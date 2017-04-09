using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 15f;
	public Transform target;
	public float damage = 5f;
	public float radius = 0;
	public GameObject explodeEffect;
	public GameObject boomEffect;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(target == null) {
			// Our enemy went away!
			Destroy(gameObject);
			return;
		}


		Vector3 dir = target.position - this.transform.localPosition;

		float distThisFrame = speed * Time.deltaTime;

		if(dir.magnitude <= distThisFrame) {
			// We reached the node
			DoBulletHit();
		}
		else {
			// TODO: Consider ways to smooth this motion.

			// Move towards node
			transform.Translate( dir.normalized * distThisFrame, Space.World );
			Quaternion targetRotation = Quaternion.LookRotation( dir );
			this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRotation, Time.deltaTime*5);
		}

	}

	void DoBulletHit() {
		// TODO:  What if it's an exploding bullet with an area of effect?

		if(radius == 0) {
			target.GetComponent<EnemyController>().TakeDamage(damage);
			GameObject boom = Instantiate(explodeEffect, transform.position, transform.rotation);
		}
		else {
			Collider[] cols = Physics.OverlapSphere(transform.position, radius);

			foreach(Collider c in cols) {
				EnemyController e = c.GetComponent<EnemyController>();
				if(e != null) {
					// TODO: You COULD do a falloff of damage based on distance, but that's rare for TD games
					GameObject boom = Instantiate(boomEffect, transform.position, transform.rotation);
					e.GetComponent<EnemyController>().TakeDamage(damage);
					Destroy (boom,2f);
				}
			}
		}

		// TODO: Maybe spawn a cool "explosion" object here?

		Destroy(gameObject);
	}
}

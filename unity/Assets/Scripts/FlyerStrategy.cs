using UnityEngine;
using System.Collections;

public class FlyerStrategy : MonoBehaviour {
	public Vector3 startPosition;
	public float speed;
	public int recycleOffset;
	// Use this for initialization
	void Start () {
		//startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled){
			Destroy(gameObject);
			return;
		}

		if (transform.position.x > startPosition.x + 5){
			speed = -speed;
		}
		else if(transform.position.x < startPosition.x - 5){
			speed = -speed;
		}
		transform.Translate (speed, 0, 0);
	}

	void OnTriggerEnter () {
		Player.updateLives(-1);
	}
}

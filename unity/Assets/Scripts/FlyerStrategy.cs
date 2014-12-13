using UnityEngine;
using System.Collections;

public class FlyerStrategy : MonoBehaviour {
	private static Vector3 startPosition;
	public float speed;
	// Use this for initialization
	void Start () {
		startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x > startPosition.x + 5){
			speed = -speed;
		}
		else if(transform.position.x < startPosition.x - 5){
			speed = -speed;
		}
		transform.Translate (speed, 0, 0);
	}
}

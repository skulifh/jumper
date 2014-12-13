using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class FlyerStrategy : MonoBehaviour {

	//Bombs
	public Transform bomb;

	public Vector3 startPosition;
	public float speed, shootTime;
	public int recycleOffset;
	private Stopwatch stopwatch;
	// Use this for initialization
	void Start () {
		//InvokeRepeating ("releaseBomb", 5, 2);
		//startPosition = transform.position;
		GameEventManager.GameOver += GameOver;
		stopwatch = Stopwatch.StartNew();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled){
			gameObject.SetActive(false);
		}

		if (transform.position.x > startPosition.x + 5){
			speed = -speed;
		}
		else if(transform.position.x < startPosition.x - 5){
			speed = -speed;
		}
		if ((stopwatch.ElapsedMilliseconds) >= shootTime){
			releaseBomb();
			stopwatch = Stopwatch.StartNew();
		}
		transform.Translate (speed, 0, 0);
	}
	
	private void GameOver () {
		gameObject.SetActive(false);
	}

	void OnTriggerEnter () {
		Player.updateLives(-1);
	}

	void releaseBomb(){
		Vector3 launch_position = new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z);
		Instantiate (bomb, launch_position, transform.rotation);
	}
}

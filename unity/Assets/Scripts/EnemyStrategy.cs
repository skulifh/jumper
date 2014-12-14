using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class EnemyStrategy : MonoBehaviour {
	public Vector3 startPosition;
	public int recycleOffset;
	public float jumpTime;
	private Stopwatch stopwatch;
	private bool gameover;
		
	// Use this for initialization
	void Start () {
		gameObject.SetActive(true);
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;
		stopwatch = Stopwatch.StartNew();
	}
	
	// Update is called once per frame
	void Update () {
		//if(transform.localPosition.x + recycleOffset < Player.distanceTraveled || gameover){
		//	Destroy(gameObject);
		//	return;
		//}
		
		rigidbody.AddForce(0, -1750 , 0 );
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled){
			gameObject.SetActive(false);
		}
		if ((stopwatch.ElapsedMilliseconds) >= jumpTime){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,30,0);
			stopwatch = Stopwatch.StartNew();
		}
	}
	
	private void GameOver () {
		gameObject.SetActive(false);
		gameover = true;
	}
	
	private void GameStart () {
		gameover = false;
	}
	
	void OnCollisionEnter (Collision other) {
		if (other.gameObject.collider.name == "Player"){
			Player.updateLives(-20);
			gameObject.SetActive(false);
		}
		//touchingPlatform = true;
		//boost = true; 
		
	}
}

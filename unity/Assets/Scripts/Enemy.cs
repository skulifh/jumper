using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class Enemy : MonoBehaviour {
	public Vector3 offset;
	public Vector3 jumpVelocity;
	public float acceleration;
	public float recycleOffset, spawnChance, jumpTime;
	
	//private Stopwatch stopwatch;
	
	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		/*rigidbody.AddForce(0, -1750 , 0 );
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}
		if ((stopwatch.ElapsedMilliseconds/1000) >= jumpTime){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,30,0);
			stopwatch = Stopwatch.StartNew();
		}*/
	}
	
	/*public void Spawn (Vector3 position) {
		if(gameObject.activeSelf || spawnChance <= Random.Range(0f, 100f)) {
			return;
		}
		stopwatch = Stopwatch.StartNew();
		transform.localPosition = position + offset;
		gameObject.SetActive(true);
	}*/
	
	private void GameOver () {
		gameObject.SetActive(false);
	}
}

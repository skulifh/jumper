using UnityEngine;
using System.Collections;

public class UnderwaterEnemyStrategy : MonoBehaviour {
	public float recycleOffset;
	public float swimSpeed;
	// Use this for initialization
	void Start () {
		gameObject.SetActive(true);
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;
	}
	
	// Update is called once per frame
	void Update () {
		if (Player.underwater && transform.localPosition.x - 10 <= Player.currentPosition.x) {
			transform.position = Vector3.MoveTowards(transform.position, Player.currentPosition, swimSpeed*Time.deltaTime);
		}
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}
	}
	
	private void GameOver () {
		gameObject.SetActive(false);
		//gameover = true;
	}
	
	private void GameStart () {
		//gameover = false;
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

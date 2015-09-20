using UnityEngine;
using System.Collections;

public class Oxygen : MonoBehaviour {
	public float recycleOffset;
	private bool gameover;
	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled || gameover){
			Destroy(gameObject);
			return;
		}
	}
	
	void OnTriggerEnter (Collider other) {
		//Runner.AddBoost();
		if (other.gameObject.collider.name == "Player"){
			Player.ResetOxygenAmount();
			Player.AddCollectPoint();
			
			//gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}
	
	private void GameOver () {
		gameover = true;
		//Destroy(gameObject);
		//return;
		
	}
	
	private void GameStart () {
		gameover = false;
	}
}

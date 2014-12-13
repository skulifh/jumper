using UnityEngine;
using System.Collections;

public class UnderwaterEnemyStrategy : MonoBehaviour {
	public float recycleOffset;
	// Use this for initialization
	void Start () {
		gameObject.SetActive(true);
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;
	}
	
	// Update is called once per frame
	void Update () {
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
}

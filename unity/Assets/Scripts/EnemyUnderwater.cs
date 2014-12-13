using UnityEngine;
using System.Collections;
//using System.Diagnostics;

public class EnemyUnderwater : MonoBehaviour {
	public float recycleOffset;
	public Transform prefab;
	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		GameEventManager.GameStart += GameStart;
		gameObject.SetActive(true);
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
	}
	
	private void GameStart () {
		gameObject.SetActive(true);
	}
	
	//Vector3 position, Vector3 scale
	public void Spawn (Vector3 position) {
		transform.localPosition = position;
		gameObject.SetActive(true);
		//Transform enemy = (Transform)Instantiate(prefab);
		//enemy.localPosition = new Vector3(position.x, position.y+2, position.z);
		//gameObject.SetActive(true);
		//Debug.Log("ovinur");
	}
}

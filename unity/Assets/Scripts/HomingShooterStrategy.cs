using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class HomingShooterStrategy : MonoBehaviour {
	
	public Transform bomb;
	public Vector3 startPosition;
	public float shootTime;
	public int recycleOffset;
	private Stopwatch stopwatch;
	
	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		stopwatch = Stopwatch.StartNew();
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled){
			gameObject.SetActive(false);
		}
		
		if ((stopwatch.ElapsedMilliseconds) >= shootTime){
			releaseBomb();
			stopwatch = Stopwatch.StartNew();
		}
	}
	
	private void GameOver () {
		gameObject.SetActive(false);
	}
	
	void OnCollisionEnter (Collision other) {
		if (other.gameObject.collider.name == "Player"){
			Player.updateLives(-20);
			gameObject.SetActive(false);
		}
		//touchingPlatform = true;
		//boost = true; 
		
	}
	
	void releaseBomb(){
		//Vector3 launch_position = new Vector3 (transform.position.x, transform.position.y - 1, transform.position.z);
		//Instantiate (bomb, launch_position, transform.rotation);
		Transform bomb_clone;
		bomb_clone = (Transform)Instantiate(bomb, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Quaternion.Euler(90, 0, 0))as Transform;
		HomingBombBehaviour homingBomb= bomb_clone.GetComponent<HomingBombBehaviour> ();
		//heading.Spawn(Player.currentPosition);
	}
}

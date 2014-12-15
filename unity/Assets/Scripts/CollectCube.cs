using UnityEngine;

public class CollectCube : MonoBehaviour {
	public float recycleOffset;
	public bool powerUp, healthUp;
	private bool gameover;
	public int amountPowerUp, amountHealthUp;

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
			Player.AddCollectPoint();
			if(powerUp){
				Player.GotFlyTime(amountPowerUp);
			}
			if(healthUp){
				Player.updateLives(amountHealthUp);
			}
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

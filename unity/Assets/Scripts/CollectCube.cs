using UnityEngine;

public class CollectCube : MonoBehaviour {
	public Vector3 offset;
	public float recycleOffset, spawnChance, powerUpChance;
	public bool powerUp=false;

	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled){
			gameObject.SetActive(false);
			//Destroy(this);
			return;
		}
	
	}
	
	void OnTriggerEnter () {
		//Runner.AddBoost();
		Player.AddCollectPoint();
		if(powerUp){
			Player.GotFlyTime();
		}
		gameObject.SetActive(false);
		//Destroy(this);
	}
	
	public void SpawnIfAvailable (Vector3 position) {
		if(spawnChance <= Random.Range(0f, 100f)) {
			if (powerUpChance <= Random.Range(0f, 100f)){
				powerUp = true;
				return;
			}
			else{
				powerUp = false;
				return;
			}
		}
		transform.localPosition = position + offset;
		transform.rotation = Quaternion.Euler(90,0,0);
		gameObject.SetActive(true);
	}

	private void GameOver () {
		gameObject.SetActive(false);
	}
}

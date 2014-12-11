using UnityEngine;

public class CollectCube : MonoBehaviour {
	public Vector3 offset;
	public float recycleOffset, spawnChance;

	// Use this for initialization
	void Start () {
		//GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}
	
	}
	
	void OnTriggerEnter () {
		//Runner.AddBoost();
		Player.AddCollectPoint();
		gameObject.SetActive(false);
	}
	
	public void SpawnIfAvailable (Vector3 position) {
		if(gameObject.activeSelf || spawnChance <= Random.Range(0f, 100f)) {
			return;
		}
		transform.localPosition = position + offset;
		gameObject.SetActive(true);
	}

	private void GameOver () {
		gameObject.SetActive(false);
	}
}

using UnityEngine;

public class CollectCube : MonoBehaviour {
	public float recycleOffset;
	public bool powerUp;

	// Use this for initialization
	void Start () {
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled){
			Destroy(gameObject);
			return;
		}
	
	}
	
	void OnTriggerEnter () {
		//Runner.AddBoost();
		Player.AddCollectPoint();
		if(powerUp){
			Player.GotFlyTime();
		}
		//gameObject.SetActive(false);
		Destroy(gameObject);
	}

	private void GameOver () {
		Destroy(gameObject);
	}
}

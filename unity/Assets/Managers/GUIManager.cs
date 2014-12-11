using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	private static GUIManager instance;
	
	public GUIText gameOverText, instructionsText, scoreText;
	
	public static int added_score = 0;
	public static float distance = 0;
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;
	}

	void Update () {
		if(Input.GetButtonDown("Jump")){
			GameEventManager.TriggerGameStart();
		}
	}
	
	private void GameStart () {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		scoreText.enabled = true;
		added_score = 0;
		//runnerText.enabled = false;
		enabled = false;
	}
	
	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		scoreText.enabled = false;
		added_score = 0;
		enabled = true;
	}
	
	public static void SetScore(float points){
		//instance.scoreText.text = points.ToString("f0");
		distance = points;
		instance.scoreText.text = (distance + added_score*20).ToString("f0");
	}
	
	public static void AddCollectToScore(){
		added_score += 1;
		instance.scoreText.text = (distance + added_score*20).ToString("f0");
	}
	
	//public static void SetBoosts(int boosts){
	//	instance.boostsText.text = boosts.ToString();
	//}

	//public static void SetDistance(float distance){
	//	instance.distanceText.text = distance.ToString("f0");
	//}
}
using UnityEngine;
using System.Diagnostics;

public class GUIManager : MonoBehaviour {
	
	private static GUIManager instance;
	
	public GUIText gameOverText, instructionsText, scoreText, flyPowerText, healthText, looseLifeText;
	
	public static int added_score = 0;
	public static float distance = 0;
	public static int flyPower = 0;
	private static Stopwatch stopwatch;
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;
		looseLifeText.enabled = false;
		healthText.enabled = false;
	}

	void Update () {
		if(Input.GetButtonDown("Jump")){
			GameEventManager.TriggerGameStart();
		}
	}
	
	private void GameStart () {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		healthText.enabled = true;
		scoreText.enabled = true;
		flyPowerText.enabled = true;
		added_score = 0;
		//runnerText.enabled = false;
		enabled = false;
	}
	
	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		scoreText.enabled = false;
		healthText.enabled = false;
		flyPowerText.enabled = false;
		instance.flyPowerText.text = "0";
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
	
	public static void SetHealth(string points){
		instance.healthText.text = points;
	}
	
	public static void PromtLostLife(){
		instance.looseLifeText.enabled = true;
		stopwatch = Stopwatch.StartNew();
	}
	
	public static void DePromtLostLife(){
		instance.looseLifeText.enabled = false;
	}

	public static void UpdateFlyPower(int flyPowerDifference){
		flyPower += flyPowerDifference;
		instance.flyPowerText.text = flyPower.ToString ("f0");
	}

	//public static void SetBoosts(int boosts){
	//	instance.boostsText.text = boosts.ToString();
	//}

	//public static void SetDistance(float distance){
	//	instance.distanceText.text = distance.ToString("f0");
	//}
}
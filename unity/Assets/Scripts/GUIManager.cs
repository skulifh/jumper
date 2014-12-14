using UnityEngine;
using System.Diagnostics;

public class GUIManager : MonoBehaviour {
	
	private static GUIManager instance;
	
	public GUIText gameOverText, bestScore, instructionsText, instructionsText2, instructionsText3, scoreText, flyPowerText, healthText, looseLifeText, welcomeText;
	
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
		bestScore.enabled = false;
		gameOverText.enabled = false;
		welcomeText.enabled = false;
		instructionsText.enabled = false;
		instructionsText2.enabled = false;
		instructionsText3.enabled = false;
		healthText.enabled = true;
		scoreText.enabled = true;
		flyPowerText.enabled = false;
		added_score = 0;
		//runnerText.enabled = false;
		enabled = false;
	}
	
	private void GameOver () {
		bestScore.text = "You scored: " + ((int)Player.distanceTraveled + Player.collected*20).ToString() + ". Your best score: " + Player.best.ToString();
		bestScore.enabled = true;
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		instructionsText2.enabled = true;
		instructionsText3.enabled = false;
		scoreText.enabled = false;
		healthText.enabled = false;
		flyPowerText.enabled = false;
		instance.flyPowerText.text = "Flypower: " + "0";
		added_score = 0;
		enabled = true;
	}
	
	public static void SetScore(float points){
		//instance.scoreText.text = points.ToString("f0");
		distance = points;
		instance.scoreText.text = "Points: " + (distance + added_score*20).ToString("f0");
	}
	
	public static void AddCollectToScore(){
		added_score += 1;
		instance.scoreText.text = (distance + added_score*20).ToString("f0");
	}
	
	public static void SetHealth(string points){
		instance.healthText.text = "Health: " + points;
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
		instance.flyPowerText.text = "Flypower: " + flyPower.ToString ("f0");
	}

	//public static void SetBoosts(int boosts){
	//	instance.boostsText.text = boosts.ToString();
	//}

	//public static void SetDistance(float distance){
	//	instance.distanceText.text = distance.ToString("f0");
	//}
}
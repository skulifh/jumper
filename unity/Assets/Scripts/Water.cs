using UnityEngine;
using System.Collections.Generic;

public class Water : MonoBehaviour {
	public float waterSurface, spawnChance, recycleOffset;
	public static float leftWall, rightWall;
	public int minLength, maxLength;
	// Use this for initialization
	void Start () {
		//this.transform.position = new Vector3(5, -6, 5);
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
		
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}
		
		if ((Player.currentPosition.y < waterSurface) && (Player.currentPosition.x > leftWall) && (Player.currentPosition.x < rightWall)) {
			Player.ToggleUnderwater();
		} else {
			Player.ToggleOverwater();
		}
		//Debug.Log(Player.currentPosition);
		
	}
	
	public void Spawn (Vector3 position) {
		if(gameObject.activeSelf || spawnChance <= Random.Range(0f, 100f)) {
			return;
		}
		transform.position = position;
		gameObject.SetActive(true);
		int length = Random.Range(minLength, maxLength);
		this.transform.Find("Bottom").transform.localScale = new Vector3(length, 1, 1);
		this.transform.Find("Bottom").transform.localPosition = new Vector3(((this.transform.Find("Bottom").transform.localScale.x)/2)-4, -12, 0);
		
		this.transform.Find("Water").transform.localScale = new Vector3(length, 13, 1);
		this.transform.Find("Water").transform.localPosition = new Vector3(((this.transform.Find("Water").transform.localScale.x)/2)-4, -6, 1);
		
		this.transform.Find("Right wall").transform.localPosition = new Vector3(((this.transform.Find("Bottom").transform.localPosition.x) + (this.transform.Find("Bottom").transform.localScale.x)/2) , this.transform.Find("Right wall").transform.localPosition.y, this.transform.Find("Right wall").transform.localPosition.z);
		
		waterSurface = this.transform.Find("Water").transform.localPosition.y + (this.transform.Find("Water").transform.localScale.y)/2;
		leftWall = this.transform.Find("Left wall").transform.position.x;
		rightWall = this.transform.Find("Right wall").transform.position.x;
		Debug.Log(leftWall);
		Debug.Log(transform.position);
		//Debug.Log(Player.currentPosition.x);
		//Debug.Log(this.transform.Find("Water").transform.localScale);
		//Debug.Log(Player.currentPosition);
		//Player.ToggleUnderwater();
	}
	
	private void GameStart () {
		//Spawn(new Vector3(27,-2,0));
	}
	
	private void GameOver () {
		gameObject.SetActive(false);
	}
}

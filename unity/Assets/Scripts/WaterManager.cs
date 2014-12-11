using UnityEngine;
using System.Collections.Generic;

public class WaterManager : MonoBehaviour {
	public float waterSurface, leftWall, rightWall;
	// Use this for initialization
	void Start () {
		//this.transform.position = new Vector3(5, -6, 5);
		waterSurface = this.transform.Find("Water").transform.localPosition.y + (this.transform.Find("Water").transform.localScale.y)/2;
		leftWall = this.transform.Find("Left wall").transform.position.x;
		rightWall = this.transform.Find("Right wall").transform.position.x;
		Debug.Log(leftWall);
		//Debug.Log(Player.currentPosition.x);
		//Debug.Log(this.transform.Find("Water").transform.localScale);
		//Debug.Log(Player.currentPosition);
		//Player.ToggleUnderwater();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Player.currentPosition.y < waterSurface) && (Player.currentPosition.x > leftWall) && (Player.currentPosition.x < rightWall)) {
			Player.ToggleUnderwater();
		} else {
			Player.ToggleOverwater();
		}
		//Debug.Log(Player.currentPosition);
		
	}
}

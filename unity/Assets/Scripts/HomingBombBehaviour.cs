using UnityEngine;
using System.Collections;

public class HomingBombBehaviour : MonoBehaviour {
	public int destroyDepth;
	public float recycleOffset;
	public float speed;
	public Vector3 heading;
	// Use this for initialization
	void Start () {
		gameObject.SetActive(true);
		heading = Player.currentPosition;
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.x + recycleOffset < Player.distanceTraveled || transform.localPosition == heading){
			gameObject.SetActive(false);
			
		}
		transform.position = Vector3.MoveTowards(transform.position, heading, speed*Time.deltaTime);
		
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.collider.name == "Player"){
			Player.updateLives(-20);
			Destroy(gameObject);
		}
		
	}
	
}

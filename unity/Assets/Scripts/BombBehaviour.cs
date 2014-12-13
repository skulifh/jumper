using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour {

	public int destroyDepth;
	public int velocity;

	// Use this for initialization
	void Start () {
		gameObject.SetActive(true);
		
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.y < -10){
			Destroy(gameObject);
			}
		rigidbody.velocity = new Vector3(0,-velocity,0);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.collider.name == "Player"){
			Player.updateLives(-20);
		}
		Destroy(gameObject);
	}
}

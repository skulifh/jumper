using UnityEngine;
using System.Collections;

public class PlatformBehaviour : MonoBehaviour {

	public bool isTransporter;
	public Vector3 startPosition;
	public float transporterSpeed;
	public int transporterLength;
	public int transporterWidth;
	public float gap;
	public int direction;
	
	private float oldLocationX;
	

	// Use this for initialization
	void Start () {
		direction = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTransporter){
			if (transform.position.x > startPosition.x + transporterLength - transporterWidth + gap){
				transporterSpeed = -transporterSpeed;
				direction = -direction;
			}
			else if(transform.position.x < startPosition.x - gap){
				transporterSpeed = -transporterSpeed;
				direction = -direction;
			}
			transform.Translate (transporterSpeed, 0, 0);
			
			if (transform.position.x > oldLocationX) {
				direction = 1;
			} else {
				direction = -1;
			}
			
			oldLocationX = transform.position.x;
		}
	}

	public void setTransporter(){
		isTransporter = true;
		renderer.material.color = Color.gray;
		collider.sharedMaterial.staticFriction2 = 1;
	}

	void OnCollisionEnter (Collision other) {
		if (isTransporter){
			if (other.gameObject.collider.name == "Player"){
				Player.setTransporter(direction * new Vector3(13/2,0,0));
			} 
		}
	}
	
	void OnCollisionStay( Collision other) {
		if (isTransporter){
			if (other.gameObject.collider.name == "Player"){
				//other.gameObject.collider.transform.Translate (transporterSpeed, 0, 0);
				Player.setTransporter(direction * new Vector3(13/2,0,0));
			} 
		}
	}
	
	void OnCollisionExit (Collision other) {
		if (isTransporter){
			if (other.gameObject.collider.name == "Player"){
				//other.gameObject.collider.transform.Translate (transporterSpeed, 0, 0);
				Player.setTransporter(direction * new Vector3(0,0,0));
			} 
		}
	}
}

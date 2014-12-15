using UnityEngine;
using System.Collections;

public class PlatformBehaviour : MonoBehaviour {

	public bool isTransporter;
	public Vector3 startPosition;
	public float transporterSpeed;
	public int transporterLength;
	public int transporterWidth;
	public float gap;
	

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isTransporter){
			if (transform.position.x > startPosition.x + transporterLength - transporterWidth + gap){
				transporterSpeed = -transporterSpeed;
			}
			else if(transform.position.x < startPosition.x - gap){
				transporterSpeed = -transporterSpeed;
			}
			transform.Translate (transporterSpeed, 0, 0);
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
				other.gameObject.collider.transform.Translate (transporterSpeed, 0, 0);
			} 
		}
	}
}

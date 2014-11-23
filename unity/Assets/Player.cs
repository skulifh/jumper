using UnityEngine;

public class Player : MonoBehaviour {

	public static float distanceTraveled;
	
	public float acceleration;
	public Vector3 jumpVelocity;
	
	private bool touchingPlatform;
	
	void Update () {
		if(Input.GetButtonDown("Jump")){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,10,0);
		}
		
		distanceTraveled = transform.localPosition.x;
		
		if(Input.GetKey(KeyCode.RightArrow)){
			//rigidbody.velocity = Vector3(10,0,0);
			rigidbody.velocity = new Vector3(5,rigidbody.velocity.y,0);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			rigidbody.velocity = new Vector3(-5,rigidbody.velocity.y,0);
		} else {
			rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
		}
	}

	//void FixedUpdate () {
	//	if(touchingPlatform){
	//		rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
	//	}
	//}

	void OnCollisionEnter () {
		touchingPlatform = true;
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}
}
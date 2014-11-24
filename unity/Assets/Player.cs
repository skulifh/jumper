using UnityEngine;

public class Player : MonoBehaviour {

	public static float distanceTraveled;
	
	public float acceleration;
	public Vector3 jumpVelocity;
	public bool boost;
	
	private bool touchingPlatform;
	
	void Start() {
		Physics.gravity = new Vector3(0, -100.0F, 0);
	}
	
	void Update () {
		if((touchingPlatform || boost) && (Input.GetButtonDown("Jump"))){
			rigidbody.velocity = new Vector3(rigidbody.velocity.x,30,0);
			if (!touchingPlatform){
				boost = false;
			}
		}

		
		distanceTraveled = transform.localPosition.x;
		
		if(Input.GetKey(KeyCode.RightArrow)){
			//rigidbody.velocity = Vector3(10,0,0);
			rigidbody.velocity = new Vector3(10,rigidbody.velocity.y,0);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			rigidbody.velocity = new Vector3(-10,rigidbody.velocity.y,0);
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
		boost = true; 
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}
}
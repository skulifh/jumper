using UnityEngine;

public class Player : MonoBehaviour {

	public static float distanceTraveled;

	
	public float acceleration;
	public Vector3 jumpVelocity;
	public bool boost;
	public static bool underwater;
	private static int collected;
	public float gameOverY;
	public static Vector3 currentPosition;
	private Vector3 startPosition;
	
	private bool touchingPlatform;
	
	void Start() {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		startPosition = transform.localPosition;
		Physics.gravity = new Vector3(0, -100.0F, 0);
		underwater = false;
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}
	
	void Update () {
		currentPosition = transform.localPosition;
		
		if((touchingPlatform || boost) && (Input.GetButtonDown("Jump"))){
			if (underwater){
				rigidbody.velocity = new Vector3(rigidbody.velocity.x,15,0);
			} else {
				rigidbody.velocity = new Vector3(rigidbody.velocity.x,30,0);
				if (!touchingPlatform){
					boost = false;
				}
				else{
					boost = true;
				}
			}
			
		}

		
		distanceTraveled = transform.localPosition.x;
		GUIManager.SetScore(distanceTraveled);
		
		if(Input.GetKey(KeyCode.RightArrow)){
			//rigidbody.velocity = Vector3(10,0,0);
			rigidbody.velocity = new Vector3(10,rigidbody.velocity.y,0);
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			rigidbody.velocity = new Vector3(-10,rigidbody.velocity.y,0);
		} else {
			rigidbody.velocity = new Vector3(0,rigidbody.velocity.y,0);
		}
		
		if(transform.localPosition.y < gameOverY){
			GameEventManager.TriggerGameOver();
		}
	}
	
	public static void ToggleUnderwater(){
		Physics.gravity = new Vector3(0, -50.0F, 0);
		underwater = true;
	}
	
	public static void ToggleOverwater(){
		Physics.gravity = new Vector3(0, -100.0F, 0);
		underwater = false;
	}
	
	private void GameStart () {
		distanceTraveled = 0f;
		transform.localPosition = startPosition;
		renderer.enabled = true;
		rigidbody.isKinematic = false;
		enabled = true;
	}
	
	private void GameOver () {
		renderer.enabled = false;
		rigidbody.isKinematic = true;
		enabled = false;
	}

	//void FixedUpdate () {
	//	if(touchingPlatform){
	//		rigidbody.AddForce(acceleration, 0f, 0f, ForceMode.Acceleration);
	//	}
	//}
	
	public static void AddCollectPoint(){
		collected += 1;
		GUIManager.AddCollectToScore();
		//GUIManager.SetBoosts(boosts);
	}

	void OnCollisionEnter () {
		touchingPlatform = true;
		boost = true; 
	}

	void OnCollisionExit () {
		touchingPlatform = false;
	}
}
using UnityEngine;
using System.Collections;

public class controller : MonoBehaviour {
	public float speed = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.LeftArrow))
		{
			rigidbody.AddForce(Vector3.left * speed, ForceMode.Impulse);
		}
		if (Input.GetKey(KeyCode.RightArrow))
		{
			rigidbody.AddForce(Vector3.right * speed, ForceMode.Impulse);
		}
		if (Input.GetKey(KeyCode.UpArrow))
		{
			rigidbody.AddForce(Vector3.up * speed, ForceMode.Impulse);
		}
		if (Input.GetKey(KeyCode.DownArrow))
		{
			rigidbody.AddForce(Vector3.down * speed, ForceMode.Impulse);
		}
	}
}

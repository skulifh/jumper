using UnityEngine;
using System.Collections;

public class BombBehaviour : MonoBehaviour {

	public int destroyDepth;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(transform.localPosition.y < -10){
			Destroy(gameObject);
			}
	}

	void OnTriggerEnter () {
		Destroy(gameObject);
	}
}

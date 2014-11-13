using UnityEngine;
using System.Collections;

public class waterManager : MonoBehaviour {
	
	public Transform prefab;
	public int dropplets = 0;
	public bool isSpawning = false;


	IEnumerator SpawnDroplet (int x, int y, int width){
			yield return new WaitForSeconds(.001f);
			
			Instantiate (prefab, new Vector3 ((Random.Range (x+-width/2*10, x+width/2*10))/10.0f, y, 0), Quaternion.Euler(0, 0, 180));
			isSpawning = false;
		}

	void Start (){
		float x = -9f;
		float y = 1.5f;
		float width = 18;
		float height = .6f;
		float droplet_width = 0.1f;
		float droplet_height = 0.1f;
		//rows
		int i, j;
		for (i = 0; i<Mathf.RoundToInt(width/droplet_width); i++)		{
			//columns
			for(j=0; j<Mathf.RoundToInt(height/droplet_height); j++){
				Instantiate (prefab, new Vector3 (x+(droplet_width*i), y+(droplet_height*j), 0), Quaternion.Euler(0, 0, 180));
				}

			}
		}


	void Update (){
		if(! isSpawning)
		{
			isSpawning = true; //Yep, we're going to spawn
			StartCoroutine(SpawnDroplet(0, 10, 18));
		}
	}
}

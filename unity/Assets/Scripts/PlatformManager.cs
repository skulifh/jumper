using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour {

	//Flyers
	public Rigidbody flyer;
	public int flyerChance;

	//Collectibles
	public Transform collectable;
	public int collectableChance;
	public int collectablePowerUpChance;


	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	
	public Enemy enemy;
	public Water water;
	
	private static bool initiationCycle;

	private Vector3 nextPosition;
	private LinkedList<Transform> objectQueue;

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new LinkedList<Transform>();
		for(int i = 0; i < numberOfObjects; i++){
			objectQueue.AddLast((Transform)Instantiate(prefab));
		}
		enabled = false;
	}
	
	void Update () {
		if(objectQueue.First.Value.localPosition.x + recycleOffset < Player.distanceTraveled){
			Recycle();
		}
		RemoveFromAboveWater();
	}
	
	private void RemoveFromAboveWater() {
		
		
		foreach (Transform platform in objectQueue) {
			//Debug.Log(platform.localPosition.x);
			//Debug.Log(Water.leftWall);
			if ((platform.localPosition.x > Water.leftWall) && (platform.localPosition.x < Water.rightWall)) {
				//Debug.Log("balblabalabl");
				Vector3 scale = new Vector3(
					Random.Range(minSize.x, maxSize.x),
					Random.Range(minSize.y, maxSize.y),
					Random.Range(minSize.z, maxSize.z));

				Vector3 position = nextPosition;
				position.x += scale.x * 0.5f;
				position.y += scale.y * 0.5f;

				platform.localScale = scale;
				platform.localPosition = position;
				
				objectQueue.AddLast(platform);

				nextPosition += new Vector3(
					Random.Range(minGap.x, maxGap.x) + scale.x,
					Random.Range(minGap.y, maxGap.y),
					Random.Range(minGap.z, maxGap.z));

				if(nextPosition.y < minY){
					nextPosition.y = minY + maxGap.y;
				}
				else if(nextPosition.y > maxY){
					nextPosition.y = maxY - maxGap.y;
				}
				objectQueue.Remove(platform);
				break;
			}
		}
	}

	private void Recycle () {
		Vector3 scale = new Vector3(
			Random.Range(minSize.x, maxSize.x),
			Random.Range(minSize.y, maxSize.y),
			Random.Range(minSize.z, maxSize.z));

		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;

		// Generate flyers by chance
		if(Player.distanceTraveled > 0 &! initiationCycle && flyerChance <= Random.Range(0f, 100f)){
			Rigidbody flyer_clone;
			flyer_clone = (Rigidbody)Instantiate(flyer, new Vector3(position.x, position.y + 10, position.z), transform.rotation);
			FlyerStrategy flyer_strategy = flyer_clone.GetComponent<FlyerStrategy> ();
			flyer_strategy.startPosition = new Vector3 (position.x, position.y + 10, position.z);
			}

		// Generate collectable by chance
		if(Player.distanceTraveled > 0 &! initiationCycle && collectableChance <= Random.Range(0f, 100f)){
			Transform collectable_clone;
				collectable_clone = (Transform)Instantiate(collectable, new Vector3(position.x, position.y + 3, position.z), Quaternion.Euler(90, 0, 0))as Transform;
			if (collectablePowerUpChance <= Random.Range(0f, 100f)){
				CollectCube collectCube = collectable_clone.GetComponent<CollectCube> ();
				collectCube.powerUp = true;
				collectCube.renderer.material.color = Color.yellow;
			}
		}

		enemy.Spawn(position);
		water.Spawn(new Vector3(position.x,-2,0));
		//}

		Transform o = objectQueue.First.Value;
		objectQueue.RemoveFirst();
		o.localScale = scale;
		o.localPosition = position;
		objectQueue.AddLast(o);

		nextPosition += new Vector3(
			Random.Range(minGap.x, maxGap.x) + scale.x,
			Random.Range(minGap.y, maxGap.y),
			Random.Range(minGap.z, maxGap.z));

		if(nextPosition.y < minY){
			nextPosition.y = minY + maxGap.y;
		}
		else if(nextPosition.y > maxY){
			nextPosition.y = maxY - maxGap.y;
		}
	}
	
	private void GameStart () {
		initiationCycle = true;
		nextPosition = startPosition;
		for(int i = 0; i < numberOfObjects; i++){
			Recycle();
		}
		initiationCycle = false;
		enabled = true;
	}

	private void GameOver () {
		enabled = false;
	}
}
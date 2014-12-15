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
	public int collectableHealthUpChance;

	//Enemies
	public Rigidbody enemy;
	public int enemyChance;
	
	//Homing enemies
	public Rigidbody homingEnemy;
	public int homingEnemyChance;
	

    //Platform
	public Transform prefab;
	public int numberOfObjects;
	public float recycleOffset;
	public Vector3 startPosition;
	public Vector3 minSize, maxSize, minGap, maxGap;
	public float minY, maxY;
	public int transporterChance;
	public int transporterWidth;
	public int minTransporterLength;
	public int maxTransporterLength;
	public float transporterSpeed;
	private bool lastTransporter;


	//public Enemy enemy;
	public Water water;
	
	private static bool initiationCycle;

	private Vector3 nextPosition;
	private LinkedList<Transform> objectQueue;
	

	void Start () {
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		objectQueue = new LinkedList<Transform>();
		lastTransporter = true;
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

		Vector3 scale = new Vector3();
		Vector3 position = nextPosition;
		position.x += scale.x * 0.5f;
		position.y += scale.y * 0.5f;
		
		if(Player.distanceTraveled > 0 &! initiationCycle && collectableChance > Random.Range(0f, 100f)){
			Transform collectable_clone;
				collectable_clone = (Transform)Instantiate(collectable, new Vector3(position.x, position.y + 3, position.z), Quaternion.Euler(90, 0, 0))as Transform;
			if (collectablePowerUpChance > Random.Range(0f, 100f)){
				CollectCube collectCube = collectable_clone.GetComponent<CollectCube> ();
				collectCube.powerUp = true;
				collectCube.renderer.material.color = Color.yellow;
			}
			if (collectableHealthUpChance > Random.Range(0f, 100f)){
				
				CollectCube collectCube = collectable_clone.GetComponent<CollectCube> ();
				collectCube.healthUp = true;
				collectCube.renderer.material.color = Color.red;
			}
		} else {
			// Generate enemy by chance
			if(Player.distanceTraveled > 0 &! initiationCycle && enemyChance > Random.Range(0f, 100f)){
				Rigidbody enemy_clone;
				enemy_clone = (Rigidbody)Instantiate(enemy, new Vector3(position.x, position.y + 1, position.z), transform.rotation);
				EnemyStrategy enemy_strategy = enemy_clone.GetComponent<EnemyStrategy> ();
				enemy_strategy.startPosition = new Vector3 (position.x, position.y + 10, position.z);
			
			}
			
			// Generate homing enemy by chance
			if(Player.distanceTraveled > 0 &! initiationCycle && homingEnemyChance > Random.Range(0f, 100f)){
				Rigidbody homing_enemy_clone;
				homing_enemy_clone = (Rigidbody)Instantiate(homingEnemy, new Vector3(position.x, position.y + 1, position.z), transform.rotation);
				HomingShooterStrategy homing_enemy_strategy = homing_enemy_clone.GetComponent<HomingShooterStrategy> ();
				homing_enemy_strategy.startPosition = new Vector3 (position.x, position.y + 10, position.z);
			
			}
			
		}
		
		// Generate flyers by chance
		if(Player.distanceTraveled > 0 &! initiationCycle && flyerChance > Random.Range(0f, 100f)){
			Rigidbody flyer_clone;
			flyer_clone = (Rigidbody)Instantiate(flyer, new Vector3(position.x, position.y + 10, position.z), transform.rotation);
			FlyerStrategy flyer_strategy = flyer_clone.GetComponent<FlyerStrategy> ();
			flyer_strategy.startPosition = new Vector3 (position.x, position.y + 10, position.z);
		}
		

		// Generate platform
		// Create platform
		Transform o = objectQueue.First.Value;
		PlatformBehaviour platformBehaviour = o.GetComponent<PlatformBehaviour> ();
		// Make platform transporter?
		if(transporterChance > Random.Range(0f, 100f) &! lastTransporter){
			platformBehaviour.isTransporter = true;
			platformBehaviour.gap = (maxGap.x + minGap.x)/2;
			platformBehaviour.renderer.material.color = Color.grey;
			int transportLength = Random.Range(minTransporterLength, maxTransporterLength);
			platformBehaviour.startPosition = position;
			platformBehaviour.transporterLength = transportLength;
			platformBehaviour.transporterWidth = transporterWidth;
			platformBehaviour.transporterSpeed = transporterSpeed;
			scale= new Vector3(transporterWidth, 1, 1);
			lastTransporter = true;
	

			nextPosition += new Vector3(
				Random.Range(minGap.x, maxGap.x) + transportLength,
				Random.Range(minGap.y, maxGap.y),
				Random.Range(minGap.z, maxGap.z));
		}
		else{
			platformBehaviour.isTransporter = false;
			platformBehaviour.renderer.material.color = Color.blue;

			scale = new Vector3(
				Random.Range(minSize.x, maxSize.x),
				Random.Range(minSize.y, maxSize.y),
				Random.Range(minSize.z, maxSize.z));

			nextPosition += new Vector3(
				Random.Range(minGap.x, maxGap.x) + scale.x,
				Random.Range(minGap.y, maxGap.y),
				Random.Range(minGap.z, maxGap.z));
			lastTransporter = false;
		}
		objectQueue.RemoveFirst();
		o.localScale = scale;
		o.localPosition = position;
		objectQueue.AddLast(o);

		if(nextPosition.y < minY){
			nextPosition.y = minY + maxGap.y;
		}
		else if(nextPosition.y > maxY){
			nextPosition.y = maxY - maxGap.y;
		}

		


		//enemy.Spawn(position);
		water.Spawn(new Vector3(position.x,-2,0));
		//}
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
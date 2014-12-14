using UnityEngine;
using System.Collections.Generic;

public class Water : MonoBehaviour {
	public float waterSurface, spawnChance, recycleOffset;
	public static float leftWall, rightWall;
	public int minLength, maxLength;
	
	//Underwtaer enemies
	public Rigidbody underwater;
	public int underwaterChance;
	
	//Oxygen
	public Transform oxygen;
	
	private static bool initiationCycle;
	//public GameObject myCube;
	//public EnemyUnderwater enemyunderwater;
	//public EnemyUnderwater underwaterenemy2;
	// Use this for initialization
	void Start () {
		//this.transform.position = new Vector3(5, -6, 5);
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameObject.SetActive(false);
		//EnemyUnderwater enemy = new EnemyUnderwater();
		//enemy.localPosition = new Vector3(1,1,1);
		//enemy.Start();
		//GameObject myCube;
		
		//http://answers.unity3d.com/questions/137354/instantiate-object-in-world-c.html
		//EnemyUnderwater cubeSpawn = (EnemyUnderwater)Instantiate(myCube, new Vector3(1,1,1), transform.rotation);
		//cubeSpawn.Spawn(new Vector3(3,3,0));
		
		//EnemyUnderwater cubeSpawn2 = (EnemyUnderwater)Instantiate(myCube, new Vector3(1,1,1), transform.rotation);
		//cubeSpawn2.Spawn(new Vector3(5,5,0));
		
	}
	
	// Update is called once per frame
	void Update () {
		//UnityEngine.Debug.Log((transform.localPosition.x + this.transform.Find("Bottom").transform.localScale.x + recycleOffset ).ToString());
		if(transform.localPosition.x + this.transform.Find("Bottom").transform.localScale.x + recycleOffset < Player.distanceTraveled){
			gameObject.SetActive(false);
			return;
		}
		
		if ((Player.currentPosition.y < waterSurface-1) && (Player.currentPosition.x > leftWall) && (Player.currentPosition.x < rightWall)) {
			Player.ToggleUnderwater();
		} else {
			Player.ToggleOverwater();
		}
		//Debug.Log(Player.currentPosition);
		
	}
	
	public void Spawn (Vector3 position) {
		if(gameObject.activeSelf || spawnChance <= Random.Range(0f, 100f) || Player.currentPosition.x < 100) {
			return;
		}
		transform.position = position;
		gameObject.SetActive(true);
		int length = Random.Range(minLength, maxLength);
		this.transform.Find("Bottom").transform.localScale = new Vector3(length, 1, 1);
		this.transform.Find("Bottom").transform.localPosition = new Vector3(((this.transform.Find("Bottom").transform.localScale.x)/2)-4, -12, 0);
		
		this.transform.Find("Lid").transform.localScale = new Vector3(length-12, 1, 1);
		this.transform.Find("Lid").transform.localPosition = new Vector3(((this.transform.Find("Lid").transform.localScale.x)/2), 1, 0);
		
		//this.transform.Find("Rail").transform.localScale = new Vector3(length-8, 1, 1);
		this.transform.Find("Rail").transform.localPosition = new Vector3(((this.transform.Find("Lid").transform.localPosition.x) - (this.transform.Find("Lid").transform.localScale.x)/2), 9, 0);
		
		this.transform.Find("Water").transform.localScale = new Vector3(length, 13, 1);
		this.transform.Find("Water").transform.localPosition = new Vector3(((this.transform.Find("Water").transform.localScale.x)/2)-4, -6, 1);
		
		this.transform.Find("Right wall").transform.localPosition = new Vector3(((this.transform.Find("Bottom").transform.localPosition.x) + (this.transform.Find("Bottom").transform.localScale.x)/2) , this.transform.Find("Right wall").transform.localPosition.y, this.transform.Find("Right wall").transform.localPosition.z);
		
		waterSurface = this.transform.Find("Water").transform.localPosition.y + (this.transform.Find("Water").transform.localScale.y)/2;
		leftWall = this.transform.Find("Left wall").transform.position.x;
		rightWall = this.transform.Find("Right wall").transform.position.x;
		
		//EnemyUnderwater cubeSpawn3 = (EnemyUnderwater)Instantiate(myCube, new Vector3(1,1,1), transform.rotation);
		//cubeSpawn3.Spawn(new Vector3(transform.position.x, transform.position.y-2, 0));
		
		// Generate enemy by chance
		//if(Player.distanceTraveled > 0 &! initiationCycle && underwaterChance <= Random.Range(0f, 100f)){
		LinkedList<UnderwaterEnemyStrategy> enemyQueue = new LinkedList<UnderwaterEnemyStrategy>();
		Rigidbody underwater_clone;
		
		int number_of_underwater = length/12;
		for (int i = 0; i < number_of_underwater; i++) {
			
			//UnityEngine.Debug.Log(new Vector3(transform.position.x + 4 + (length)*i/number_of_underwater, transform.position.y-2, 0));
			underwater_clone = (Rigidbody)Instantiate(underwater, new Vector3(transform.position.x + 4 + (length)*i/number_of_underwater, Random.Range(transform.position.y, transform.position.y-10), 0), transform.rotation);
			
			enemyQueue.AddLast(underwater_clone.GetComponent<UnderwaterEnemyStrategy> ());
		}
		
		int number_of_oxygen = length/48;
		LinkedList<Oxygen> oxygenQueue = new LinkedList<Oxygen>();
		Transform oxygen_clone;
		
		for (int i = 0; i < number_of_oxygen; i++) {
		
			oxygen_clone = (Transform)Instantiate(oxygen, new Vector3(transform.position.x + 2 + (length)*i/number_of_oxygen, transform.position.y-2, 0), Quaternion.Euler(90, 0, 0))as Transform;
			oxygenQueue.AddLast(oxygen_clone.GetComponent<Oxygen> ());
		
		}
		
		//Rigidbody underwater_clone;
		//underwater_clone = (Rigidbody)Instantiate(underwater, new Vector3(transform.position.x, transform.position.y-2, 0), transform.rotation);
		//UnderwaterEnemyStrategy enemy_strategy = underwater_clone.GetComponent<UnderwaterEnemyStrategy> ();
			//enemy_strategy.startPosition = new Vector3(transform.position.x, transform.position.y-2, 0);
		
		//}
		
		//EnemyUnderwater prefab = Resources.LoadAssetAtPath<EnemyUnderwater>("Assets/Prefabs/Enemy underwater.prefab");
		//GameObject bla = Instantiate(prefab) as GameObject;
		
		//bla.SetActive(true);
		
		//bla.localPosition = this.transform.Find("Bottom").transform.localPosition;
		//Transform prefab = Instantiate(Resources.LoadAssetAtPath("Assets/Prefabs/Enemy underwater.prefab", typeof(GameObject))) as Transform;
		//prefab.localPosition = this.transform.Find("Bottom").transform.localPosition;
		
		//enemyunderwater.Spawn(this.transform.Find("Bottom").transform.localPosition, this.transform.Find("Bottom").transform.localScale);
		//Transform enemy = Instantiate(underwaterenemy2) as Transform;
		
		
		//enemy.localPosition = this.transform.Find("Bottom").transform.localPosition;
		
		Debug.Log(leftWall);
		Debug.Log(transform.position);
		//Debug.Log(Player.currentPosition.x);
		//Debug.Log(this.transform.Find("Water").transform.localScale);
		//Debug.Log(Player.currentPosition);
		//Player.ToggleUnderwater();
	}
	
	private void GameStart () {
		//Spawn(new Vector3(27,-2,0));
	}
	
	private void GameOver () {
		gameObject.SetActive(false);
		leftWall = 0;
		rightWall = 0;
	}
}

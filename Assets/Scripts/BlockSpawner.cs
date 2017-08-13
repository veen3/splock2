using UnityEngine;
using System.Collections;

public class BlockSpawner : MonoBehaviour {

	private PlayerController _pc;

	public GameObject blockPrefab;
	public GameObject portalPrefab;

	public float spawnRate = 0.50f; //time in seconds between each block spawns
	public float spawnTime = 0.50f; //set to same as spawnRate
	private int spawnOnPlayer = 2; //every x block will spawn use players x/y coordinates to prevent large empty areas

	public int spawnedBlocks = 0;

	public bool portalSpawned = false;

	public int stage = 0; //current stage/level
	private int colorRange = 2;
	private int portalGap = 25;

	public Material blockMaterial;
	public Material playerMaterial;

	public Color[] blockColors;

	public int currentColor = 0;
	     
	private Transform playerPos;

	void Start(){
		_pc = GameObject.Find("Player").GetComponent<PlayerController>();
		playerPos = GameObject.Find("Sphere").GetComponent<Transform>();
	}

	void Update () {
		if(Time.timeSinceLevelLoad > spawnRate && !_pc.playerCollided && _pc.startGame && _pc.currentSpeed > (_pc.travelSpeed * 0.50f)){
			SpawnBlock();
		}
	}

	public void SpawnBlock(){

		spawnRate = Time.timeSinceLevelLoad + spawnTime;

		float angle = Random.Range(0, 359.9f);
		float posX = (transform.localScale.x / 2) * Mathf.Cos(angle) + transform.position.x;
		float posY = (transform.localScale.y / 2) * Mathf.Sin(angle) + transform.position.y;

		Vector3 spawnPos;

		GameObject spawnBlock;

		if(spawnOnPlayer == 0){
			spawnPos = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);
			spawnOnPlayer = 3;
		}else{
			spawnPos = new Vector3(posX, posY, transform.position.z);
			spawnOnPlayer--;
		}

		if(!portalSpawned){
			if(spawnedBlocks < portalGap){
				spawnBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity) as GameObject;

				int rnColor = Random.Range(stage, stage + colorRange + 1);

				Debug.Log("Color: " + rnColor);

				spawnBlock.GetComponent<MeshRenderer>().material.color = blockColors[rnColor];

				spawnedBlocks++;	
			}else{
				Instantiate(portalPrefab, transform.position + new Vector3(0,0,500), Quaternion.identity);

				spawnedBlocks = 0;
				portalGap += portalGap + 75;
			}
		}else{
			spawnBlock = Instantiate(blockPrefab, spawnPos, Quaternion.identity) as GameObject;

			int rnColor = Random.Range(stage, stage + colorRange + 1);

			Debug.Log("Color: " + rnColor);

			spawnBlock.GetComponent<MeshRenderer>().material.color = blockColors[rnColor];
		}

	}
}

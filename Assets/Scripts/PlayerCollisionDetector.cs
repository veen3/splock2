using UnityEngine;
using System.Collections;

public class PlayerCollisionDetector : MonoBehaviour {

	private PlayerController _pc;

	public GameObject explosionPrefab;

	void Start(){
		_pc = GameObject.Find("Player").GetComponent<PlayerController>();
	}

	void OnTriggerEnter(Collider coll){
		if(coll.gameObject.tag == "Block"){
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);

			_pc.StopGame();
		}
	}

}

using UnityEngine;
using System.Collections;

public class BlockRemover : MonoBehaviour {

	private BlockSpawner _bs;

	void Start(){
		_bs = GameObject.Find("BlockSpawner").GetComponent<BlockSpawner>();
	}

	void OnTriggerEnter(Collider coll){
		if(coll.gameObject.tag == "Block"){
			Destroy(coll.gameObject);	
		}

		if(coll.gameObject.tag == "Portal"){
			if(_bs.spawnRate > 0.15f){
				_bs.spawnRate -= 0.05f;
				_bs.spawnTime -= 0.05f;	
			}

			if(_bs.stage == 9){
				_bs.stage = 0;	
			}
			_bs.stage += 3;
		}

		if(coll.gameObject.tag == "PortalEnd"){
			Destroy(coll.transform.parent.gameObject);
		}

	}

}

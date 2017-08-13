using UnityEngine;
using System.Collections;

public class PortalMover : MonoBehaviour {

	public bool enableMoving;

	public float moveX;
	public float moveY;
	public float moveZ;

	void Update () {
		if(enabled){
			transform.position += new Vector3(moveX,moveY,moveZ) * Time.deltaTime;
		}
	}
}

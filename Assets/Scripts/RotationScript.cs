using UnityEngine;
using System.Collections;

public class RotationScript : MonoBehaviour {

	public bool delayRotation;

	public float timeDelay;

	public float rotateSpeedX;
	public float rotateSpeedY;
	public float rotateSpeedZ;

	// Update is called once per frame
	void Update () {
		if(delayRotation){
			if(Time.timeSinceLevelLoad > timeDelay){
				transform.Rotate(new Vector3(rotateSpeedX,rotateSpeedY,rotateSpeedZ) * Time.deltaTime);		
			}
		}else{
			transform.Rotate(new Vector3(rotateSpeedX,rotateSpeedY,rotateSpeedZ) * Time.deltaTime);
		}

	}
}

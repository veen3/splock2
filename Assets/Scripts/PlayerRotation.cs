using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {

	Quaternion rotation;

	// Use this for initialization
	void Awake() {
		rotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = rotation;
	}

}

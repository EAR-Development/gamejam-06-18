using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficJamController : MonoBehaviour {
	enum Mode {playeroutside, playerinside};
	private Mode currentMode = Mode.playeroutside;

	private float jamSpeed = 0.5f;


	public void honk(){
	}

	public void playerEnter(){
		print("Jam start");
		currentMode = Mode.playerinside;
	}

	public void playerLeft(){
		print("Jam end");
		currentMode = Mode.playeroutside;
	}

	public float getJamSpeed(){
		return jamSpeed;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentMode == Mode.playerinside) {
			transform.Translate (Vector3.forward * jamSpeed);
		}
	}
}

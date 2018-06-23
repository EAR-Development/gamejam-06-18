using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficJamController : MonoBehaviour {
	enum Mode {playeroutside, playerinside};
	private Mode currentMode = Mode.playeroutside;

	private float jamSpeed = 0.5f;

	private float angerlevel = 0.1f;

	public float maxSpeed = .5f;
	public float endSpeed = 0.3f;
	public float sweetSpot = 0.7f;
	public float honkFactor = .2f;

	private float offsetfunction(float startx, float starty, float endx, float endy, float x){
		float t = endy - starty;
		float l = endx - startx;

		return (-2 * t) / (l * l * l) * (x * x * x) + (3 * t) / (l * l) * (x * x);
	}

	private float angerLevel2Speed(float angerLevel){
		if (angerLevel < 0) {
			return 0;
		}
		else if (angerlevel >= 1){
			return endSpeed;
		}
		else if (angerlevel >= 0 && angerlevel < sweetSpot){
			return offsetfunction(0, 0, sweetSpot, maxSpeed, angerlevel);
		}
		else if (sweetSpot >= 0 && angerlevel < 1){
			return offsetfunction(sweetSpot, maxSpeed, 1, endSpeed, angerlevel);
		}
		else{
			return 0;
		}
	}

	private float angerlevelToHonkEfficiency(float level){
		return level;
	}

	public void honk(){
		angerlevel += angerlevelToHonkEfficiency (angerlevel)*honkFactor;
		angerlevel = Mathf.Min (1, Mathf.Max (0, angerlevel));
		print (angerlevel);
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
		return angerLevel2Speed(angerlevel);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (currentMode == Mode.playerinside) {
			// angerlevel -= 0.01f;
			angerlevel = Mathf.Min (1, Mathf.Max (0, angerlevel));
			transform.Translate (Vector3.forward * getJamSpeed());
		}
	}
}

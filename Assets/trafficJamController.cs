using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trafficJamController : MonoBehaviour {
	enum Mode {playeroutside, playerinside};
	private Mode currentMode = Mode.playeroutside;

	private float jamSpeed = 0.5f;

	private float angerlevel = 0.1f;
	private float nextangerlevel = 0.1f;

	private float maxSpeed = .5f;
	private float endSpeed = 0.3f;
	private float sweetSpot = 0.7f;
	private float honkFactor = .05f;

	public GameObject emergencyPrefab;
	public GameObject emergencySpawn;

	private GameObject emergency;
	private jamCar[] cars;
	private playerScript assignedPlayer;

	private float offsetfunction(float startx, float starty, float endx, float endy, float x){
		float t = endy - starty;
		float l = endx - startx;

		x = x - startx;

		return (-2 * t) / (l * l * l) * (x * x * x) + (3 * t) / (l * l) * (x * x) + starty;
	}

	private float angerLevel2Speed(float level){
		if (level < 0) {
			return 0;
		}
		else if (level >= 1){
			return endSpeed;
		}
		else if (level >= 0 && level < sweetSpot){
			return offsetfunction(0, 0, sweetSpot, maxSpeed, level);
		}
		else if (level >= sweetSpot && level <= 1){
			return offsetfunction(sweetSpot, maxSpeed, 1, endSpeed, level);
		}
		else{
			return 0;
		}
	}

	private float angerlevelToHonkEfficiency(float level){
		return level + 0.1f;
	}

	public void registerPlayer(playerScript player){
		assignedPlayer = player;
	}

	public void honk(){
		nextangerlevel = angerlevel + angerlevelToHonkEfficiency (angerlevel)*honkFactor;
		nextangerlevel = Mathf.Min (1, Mathf.Max (0, nextangerlevel));
	}

	public void playerEnter(){
		currentMode = Mode.playerinside;
		emergency = GameObject.Instantiate (emergencyPrefab, emergencySpawn.transform.position, emergencySpawn.transform.rotation);
		emergency.transform.parent = gameObject.transform;
	}

	public void playerLeft(){
		currentMode = Mode.playeroutside;
	}

	public float getJamSpeed(){
		return angerLevel2Speed(angerlevel);
	}

	void Start(){
		cars = GetComponentsInChildren<jamCar> ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		if (currentMode == Mode.playerinside) {
			angerlevel = nextangerlevel - 0.001f;
			angerlevel = Mathf.Min (1, Mathf.Max (0, angerlevel));
			transform.Translate (Vector3.forward * getJamSpeed());
			nextangerlevel = angerlevel;
			emergency.transform.Translate (Vector3.forward * 0.3f);

			foreach(jamCar car in cars) {
				car.moveForEmergency (emergency.transform);
			}

			assignedPlayer.moveForEmergency (emergency.transform);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jamCar : MonoBehaviour {

	private float offsetfunction(float startx, float starty, float endx, float endy, float x){
		float t = endy - starty;
		float l = endx - startx;

		x = x - startx;

		return (-2 * t) / (l * l * l) * (x * x * x) + (3 * t) / (l * l) * (x * x) + starty;
	}


	float middleXPosition;
	float initialAngle;
	float lastXPosition;

	public void moveForEmergency(Transform emergencyPosition, float currentJamSpeed){
		Vector3 currentPosition = transform.position;
		float distance = Mathf.Abs(emergencyPosition.position.z - transform.position.z);
		if (distance < 100) {
			currentPosition.x = middleXPosition - offsetfunction (0, 0, 100, -2, 100-distance);
		}
		transform.position = currentPosition;

		float xDelta = currentPosition.x - lastXPosition;
		float yDelta = currentJamSpeed;

		float angle = Mathf.Rad2Deg * Mathf.Atan (xDelta / yDelta);

		transform.rotation = Quaternion.Euler (new Vector3 (0, initialAngle + angle, 0));

		lastXPosition = currentPosition.x;

	}

	// Use this for initialization
	void Start () {
		middleXPosition = transform.position.x;
		lastXPosition = middleXPosition;

		initialAngle = transform.rotation.eulerAngles.y;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

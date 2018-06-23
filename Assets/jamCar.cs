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

	public void moveForEmergency(Transform emergencyPosition){
		Vector3 currentPosition = transform.position;
		float distance = Mathf.Abs(emergencyPosition.position.z - transform.position.z);
		if (distance < 100) {
			currentPosition.x = middleXPosition - offsetfunction (0, 0, 100, -2, 100-distance);
		}
		transform.position = currentPosition;

	}

	// Use this for initialization
	void Start () {
		middleXPosition = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

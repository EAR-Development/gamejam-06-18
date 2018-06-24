using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emergencyCar : MonoBehaviour {

	private Light[] lights;
	private float timer;

	// Use this for initialization
	void Start () {
		lights = GetComponentsInChildren<Light> ();

		lights[0].intensity = 0;
		lights[1].intensity = 0;
	}
	
	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime;

		if (timer < .5f) {
			lights [0].intensity = 6;
			lights [1].intensity = 0;
		} else {
			lights [0].intensity = 0;
			lights [1].intensity = 6;
		}

		timer = timer % 1;
	}
}

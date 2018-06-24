using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emergencyDespawnScript : MonoBehaviour {
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<emergencyCar> () != null) {
			print ("emergency Car");
			GetComponentInParent<trafficJamController> ().newEmergency();
		}
	}
}

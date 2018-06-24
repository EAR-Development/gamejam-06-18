using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelLineScript : MonoBehaviour {

	public bool removesEmergencies = false;

	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<playerScript> () != null) {
			other.GetComponent<playerScript>().enterTunnel();
		}
		else if (other.GetComponent<jamCar> () != null) {
			other.gameObject.SetActive(false);
		}
		else if (removesEmergencies && other.GetComponent<emergencyCar> () != null) {
			other.gameObject.SetActive(false);
		}
	}
}

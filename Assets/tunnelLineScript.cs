using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelLineScript : MonoBehaviour {
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<playerScript> () != null) {
			other.GetComponent<playerScript>().enterTunnel();
		}
		else if (other.GetComponent<jamCar> () != null) {
			other.GetComponent<MeshRenderer> ().enabled = false;
		}
	}
}

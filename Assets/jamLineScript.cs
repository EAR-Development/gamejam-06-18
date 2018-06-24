using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jamLineScript : MonoBehaviour {
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<playerScript> () != null) {
			other.GetComponent<playerScript>().enterTrafficJam();
		}
	}
}

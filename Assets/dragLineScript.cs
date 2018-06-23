using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragLineScript : MonoBehaviour {
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<playerScript> () != null) {
			other.GetComponent<playerScript>().enterDragRace();
		}
	}
}

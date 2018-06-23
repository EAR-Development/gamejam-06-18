using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalScript : MonoBehaviour {
    private void OnTriggerEnter(Collider other)
    {
		if (other.GetComponent<playerScript> () != null) {
			other.GetComponent<playerScript>().stoptime();
		}
    }
}

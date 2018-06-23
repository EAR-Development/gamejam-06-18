using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jamLineScript : MonoBehaviour {
	private void OnTriggerEnter(Collider other)
	{
		other.GetComponent<playerScript>().enterTrafficJam();
	}
}

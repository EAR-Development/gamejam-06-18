using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tunnelLineScript : MonoBehaviour {
	private void OnTriggerEnter(Collider other)
	{
		other.GetComponent<playerScript>().enterTunnel();
	}
}

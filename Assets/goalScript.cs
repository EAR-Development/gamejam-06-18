﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goalScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<playerScript>().stoptime();
    }
}
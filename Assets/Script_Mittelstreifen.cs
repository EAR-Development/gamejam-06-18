using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Mittelstreifen : MonoBehaviour {

    public GameObject Kurz;
	// Use this for initialization
	void Start () {
        for ( int i = 0; i < 200; i++)
        {
            GameObject Strich = Instantiate(Kurz, transform, true);
            Strich.transform.position = new Vector3(0, 1, i*3-20);
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

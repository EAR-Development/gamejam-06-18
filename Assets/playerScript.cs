using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using UnityEngine;
using UnityEngine.UI;

public class playerScript : MonoBehaviour {

	public enum Mode {start, drag, tunnel, jam, finish};

	public bool isPlayerOne = true;
	private string playerButton;
    public GameObject drehzahlMesser;
    public GameObject showStopwatch;
    //public float ZeitStopper = 0f;
    public Stopwatch ZeitStopper;
    public Text player_1_time;
    public float maximumSpeed = 20;
	private float currentSpeed = 0;

	private float jamSpeed = 0.5f;

	public Mode currentMode = Mode.start;

	private float currentMotorPower = 0f;

	// Use this for initialization
	void Start () {
		if (isPlayerOne) {
			playerButton = "player_1";
		} else {
			playerButton = "player_2";
		}
        ZeitStopper = new Stopwatch();
        ZeitStopper.Start();

		currentMode = Mode.drag;
    }

	public void enterTrafficJam(){
		currentMode = Mode.jam;
	}

	public void enterTunnel(){
		currentMode = Mode.tunnel;
	}

    public void stoptime()
    {
        print("trigger");
        TimeSpan ts = ZeitStopper.Elapsed;
        ZeitStopper.Stop();
        print(ts);
        ZeitStopper.Reset();
    }
	
	// Update is called once per frame
	void Update () {
        player_1_time.text = Convert.ToString(ZeitStopper.Elapsed);

    }


    void FixedUpdate(){
		if (currentMode == Mode.drag) {
			// automatic Speedup
			currentMotorPower = Mathf.MoveTowards (currentMotorPower, 1.0f, 0.01f);

			if (Input.GetButtonDown (playerButton)) {
				currentMotorPower = 0.1f;
			}
				
			float acceleration = currentMotorPower;
			if (currentMotorPower > .8f) {
				acceleration = -4 * currentMotorPower + 4;
			}

			currentSpeed = Mathf.MoveTowards (currentSpeed, maximumSpeed, currentMotorPower * acceleration * 0.1f);


			int pixel;

			int relPixel = (int)Mathf.Round (currentMotorPower * 600f);
			if (isPlayerOne) {
				pixel = relPixel + 30;
			} else {
				pixel = relPixel - 630;
			}

			drehzahlMesser.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (pixel, 50);

			// UPDATE POSITION OF UI PIN -> currentMotorPower
		} else if (currentMode == Mode.tunnel) {
			currentSpeed -= 0.05f;
			currentSpeed = Mathf.Max (0, jamSpeed);
		}

		// Move Car
		this.transform.Translate (Vector3.forward * currentSpeed);

		currentSpeed -= 0.005f;
		currentSpeed = Mathf.Max (0, currentSpeed);
	}
}

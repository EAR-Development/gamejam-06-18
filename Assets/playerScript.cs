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

	public trafficJamController jamscript;

	public bool isPlayerOne = true;
	private string playerButton;
    public GameObject drehzahlMesser;
    public GameObject angerOut;
    public GameObject showStopwatch;
    //public float ZeitStopper = 0f;
    public Stopwatch ZeitStopper;
    public Text player_1_time;
    public float maximumSpeed = 20;
	private float currentSpeed =0;

	public GameObject backMirrorCamera;

	public AudioSource source;
	public AudioSource honksource;
	public AudioClip honk;

    private GameObject pSpeed;
    private GameObject pMesser;

	private float jamSpeed = 0.5f;

	public Mode currentMode = Mode.start;

	private float middleXPosition;
	float lastXPosition;
	private bool honked;
	int honkNumber = 0;

	private float currentMotorPower = 0f;

	private float offsetfunction(float startx, float starty, float endx, float endy, float x){
		float t = endy - starty;
		float l = endx - startx;

		x = x - startx;

		return (-2 * t) / (l * l * l) * (x * x * x) + (3 * t) / (l * l) * (x * x) + starty;
	}

	// Use this for initialization
	void Start () {
		// backMirrorCamera.SetActive (false);
		if (isPlayerOne) {
			playerButton = "player_1";
            pSpeed = GameObject.Find("player_1_speed");
			angerOut = GameObject.Find("player_1_anger");
            pMesser = GameObject.Find("Messer_1");
        } else {
			playerButton = "player_2";
            pSpeed = GameObject.Find("player_2_speed");
			angerOut = GameObject.Find("player_2_anger");
            pMesser = GameObject.Find("Messer_2");


        }
		pSpeed.SetActive(true);
		pMesser.SetActive(true);
		angerOut.SetActive(false);

        ZeitStopper = new Stopwatch();
        ZeitStopper.Start();

		currentMode = Mode.drag;

		source = GetComponentsInChildren<AudioSource>()[0];
		honksource = GetComponentsInChildren<AudioSource>()[1];
		source.Play ();
		source.volume = .2f;

		jamscript.registerPlayer (GetComponent<playerScript> ());
		middleXPosition = transform.position.x;
		lastXPosition = middleXPosition;
    }

	public void enterTrafficJam(){
		// backMirrorCamera.SetActive (true);
        pSpeed.SetActive(false);
		pMesser.SetActive(true);
        angerOut.SetActive(true);
        currentMode = Mode.jam;
		jamscript.playerEnter ();
	}

	public void enterTunnel(){
		// backMirrorCamera.SetActive (false);
        pSpeed.SetActive(false);
        pMesser.SetActive(false);
        angerOut.SetActive(false);

        currentMode = Mode.tunnel;
		jamscript.playerLeft ();
	}

	public void enterDragRace(){
		// backMirrorCamera.SetActive (false);
        pSpeed.SetActive(true);
        pMesser.SetActive(true);
        angerOut.SetActive(false);
        currentMode = Mode.drag;
		jamscript.playerLeft ();
	}

    public void stoptime()
    {
        print("trigger");
        TimeSpan ts = ZeitStopper.Elapsed;
        ZeitStopper.Stop();
        print(ts);

		currentMode = Mode.finish;
    }
	
	// Update is called once per frame
	void Update () {
        player_1_time.text = Convert.ToString(ZeitStopper.Elapsed).Substring(3,9);
    }

	public void moveForEmergency(Transform emergencyPosition){
		Vector3 currentPosition = transform.position;
		float distance = Mathf.Abs(emergencyPosition.position.z - transform.position.z);
		if (distance < 100) {
			currentPosition.x = middleXPosition - offsetfunction (0, 0, 100, -2, 100-distance);
			if (honked) {
				honkNumber++;
			}
		}
		transform.position = currentPosition;

		float xDelta = currentPosition.x - lastXPosition;
		float yDelta = jamscript.getJamSpeed();

		float angle = Mathf.Rad2Deg * Mathf.Atan (xDelta / yDelta);

		transform.rotation = Quaternion.Euler (new Vector3 (0, angle, 0));

		lastXPosition = currentPosition.x;


		if (distance < 3 && honkNumber > 10) {
			print ("You're a Honk");
			jamscript.resetLevel ();
			honkNumber = -5;
		}
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

			int relPixel = (int)Mathf.Round (currentMotorPower * 480f);
			if (isPlayerOne) {
				pixel = relPixel + 10;
			} else {
				pixel = relPixel - 510;
			}

			drehzahlMesser.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (pixel, 50);

			currentSpeed -= 0.005f;

			// UPDATE POSITION OF UI PIN -> currentMotorPower
		} else if (currentMode == Mode.tunnel) {
			currentSpeed -= 0.1f;
			currentSpeed = Mathf.Max (jamscript.getJamSpeed(), currentSpeed);
			currentMotorPower = 0.2f;
		} else if (currentMode == Mode.jam) {
			honked = false;
			if (Input.GetButtonDown (playerButton)) {
				jamscript.honk ();
				honked = true;
				honksource.PlayOneShot (honk);
			}
			currentSpeed = jamscript.getJamSpeed ();
			currentMotorPower = 0.2f;
		} else if (currentMode == Mode.finish) {
			currentSpeed = 0f;
			currentMotorPower = 0f;
		}



		// Move Car
		this.transform.Translate (Vector3.forward * currentSpeed);
	
		source.pitch = currentMotorPower * 0.8f + 0.2f;

		currentSpeed = Mathf.Max (0, currentSpeed);
	}

    public void setAngerOut(float level)
    {

        int pixel;

        int relPixel = (int)Mathf.Round(level * 480f);
        if (isPlayerOne)
        {
            pixel = relPixel + 10;
        }
        else
        {
			pixel = relPixel - 490;
        }

		pMesser.GetComponent<RectTransform> ().anchoredPosition = new Vector2 (pixel, 50);
    }
}

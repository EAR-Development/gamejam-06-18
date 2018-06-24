using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class endscreenScript : MonoBehaviour {

	TimeSpan player1Endtime;
	TimeSpan player2Endtime;

	bool player1set = false;
	bool player2set = false;

	public GameObject endscreen;

	public void Start(){
		endscreen.SetActive (false);
	}

	public void registerTime(TimeSpan time, bool isPlayerOne){
		if (isPlayerOne && !player1set) {
			player1Endtime = time;
			player1set = true;
		}
		if (!isPlayerOne && !player2set) {
			player2Endtime = time;
			player2set = true;
		}
		if(player1set && player2set){
			endscreen.SetActive (true);
			GameObject.Find ("player_1_endtime").GetComponent<Text>().text = player1Endtime.ToString();
			GameObject.Find ("player_2_endtime").GetComponent<Text>().text = player2Endtime.ToString();
		}
	}

	public void restartGame(){
		SceneManager.LoadScene("scenes/main");
	}

	public void FixedUpdate(){
		if (player1set && player2set && (Input.GetButtonDown ("player_1") || Input.GetButtonDown ("player_2"))) {
			restartGame ();
		}
	}
}

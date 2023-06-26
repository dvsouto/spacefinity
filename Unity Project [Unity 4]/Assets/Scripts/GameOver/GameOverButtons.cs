using UnityEngine;
using System.Collections;

public class GameOverButtons : MonoBehaviourGame {

	private AudioSource buttonClickSound;

	// Use this for initialization
	void Start () {
		this.buttonClickSound = GameObject.Find("ButtonClickSound").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlayAgain()
	{
		this.getGameControl().restartGame();
		this.buttonClickSound.Play();
	}

	public void MainMenu()
	{
		this.buttonClickSound.Play();
		GameObject.FindGameObjectWithTag("AdMob").GetComponent<GoogleAdMob>().destroyAdmob();
		Application.LoadLevel("MainMenu");
	}
}

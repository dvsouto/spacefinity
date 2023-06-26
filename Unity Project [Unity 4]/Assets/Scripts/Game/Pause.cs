using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviourGame {

	private bool isPaused = false;

	public Text pauseText;
	public Sprite pauseSprite, playSprite;

	private AudioSource buttonClickSound;

	void Start()
	{
		this.buttonClickSound = GameObject.Find("ButtonClickSound").GetComponent<AudioSource>();
	}

	public void tooglePause()
	{
		if (this.SystemControl.gameState == GameState.Play) this.PauseGame();
		else                                                this.PlayGame();  

		pauseText.enabled = isPaused;
		this.buttonClickSound.Play();
	}

	private void PauseGame()
	{
		isPaused = true;
		this.SystemControl.gameState = GameState.Pause;
		Time.timeScale = 0;

		this.GetComponent<Image>().sprite = playSprite;
		this.getGameControl().GetComponent<AudioSource>().Pause();
	}

	private void PlayGame()
	{
		isPaused = false;
		this.SystemControl.gameState = GameState.Play;
		Time.timeScale = 1;

		this.GetComponent<Image>().sprite = pauseSprite;
		this.getGameControl().GetComponent<AudioSource>().Play();
	}
}

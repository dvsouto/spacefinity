using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverControl : MonoBehaviourGame {

	public GameObject GameOverBackground;
	public GameObject BottomPanel;
	public GameObject Score;
	public GameObject BestScore;

	public GameObject PlayAgainButton, MainMenuButton;

	// private GoogleAdMob admob;

	// Use this for initialization
	void Start () {
		if (GameOverBackground == null) GameObject.Find ("GameOverBackground");
		if (BottomPanel == null) GameObject.Find ("BottomGameOverPanel");
		if (Score == null) GameObject.Find ("Score");
		if (BestScore == null) GameObject.Find ("BestScore");

		// Carregar idioma ///////////////////////////////////////////
		Text textComp;
		
		// Play Again
		textComp = PlayAgainButton.GetComponentInChildren<Text>();
		textComp.text = Language.languageVars.game_over.PlayAgain.label;
		textComp.fontSize = Language.languageVars.game_over.PlayAgain.size;
		
		// Main Menu
		textComp = MainMenuButton.GetComponentInChildren<Text>();
		textComp.text = Language.languageVars.game_over.MainMenu.label;
		textComp.fontSize = Language.languageVars.game_over.MainMenu.size;

		// admob = GameObject.FindGameObjectWithTag("AdMob").GetComponent<GoogleAdMob>();
	}

	public void setScore(int Score)
	{
		int bestScore = PlayerPrefs.GetInt("BestScore", 0);

		// Melhor pontuacao
		if (Score > bestScore) 
		{
			PlayerPrefs.SetInt("BestScore", Score);
			bestScore = Score;
		}

		// Setar pontuacoes
		this.Score.GetComponent<Text>().text = Score.ToString();
		this.BestScore.GetComponent<Text>().text = bestScore.ToString();
	}

	public void show(bool showAnim = true)
	{
		this.gameObject.SetActive(true);
		//if (admob != null) admob.showBanner();

		if (showAnim)
		{
			GameOverBackground.GetComponent<Animator>().SetBool("moveMainPanel", true);
			BottomPanel.GetComponent<Animator>().SetBool("moveBottomPanel", true);
		}
	}

	public void hide(bool hideAnim = true)
	{
		this.gameObject.SetActive(false);
		//if (admob != null) admob.hideBanner();

		if (hideAnim)
		{
			GameOverBackground.GetComponent<Animator>().SetBool("moveMainPanel", false);
			BottomPanel.GetComponent<Animator>().SetBool("moveBottomPanel", false);
		}
	}


}

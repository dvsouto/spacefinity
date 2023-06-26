using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviourGame {

	public bool changeBGColor = false;

	private Image background;
	private Color maxColor = Color.white;
	private Color minColor = new Color(0.72f, 0.72f, 0.72f);

	private bool colorDirection = false;
	private float timeToColor = 3.8f;
	private float currentTimeToColor;
	 
	// Use this for initialization
	void Start () {
		if (changeBGColor) 
		{
			this.background = this.GetComponent<Image>();
			currentTimeToColor = timeToColor;
		}

		GameObject.Find("BestScoreTxt").GetComponent<Text>().text = PlayerPrefs.GetInt("BestScore", 0).ToString();
		GameObject.Find("QualitySlider").GetComponent<Slider>().value = PlayerPrefs.GetInt("quality", 1);

		if (PlayerPrefs.GetInt("quality", 1) == 1) QualitySettings.antiAliasing = 2;
		else QualitySettings.antiAliasing = 0;  

		setMenuText();
	}
	
	// Update is called once per frame
	void Update () {

		if (changeBGColor)
		{
			currentTimeToColor += Time.deltaTime;

			if (currentTimeToColor >= timeToColor)
			{
				colorDirection = ! colorDirection;

				if (colorDirection) background.CrossFadeColor(minColor, timeToColor - 0.25f, false, false);
				else                background.CrossFadeColor(maxColor, timeToColor - 0.25f, false, false);

				currentTimeToColor = 0;
			}
		} 
	}

	public void setMenuText()
	{
		Text textComp;

		// Main menu //////////////////////////////////////////////////////////////////
		textComp = GameObject.Find("StartGame").GetComponentInChildren<Text>();
		textComp.text     = Language.languageVars.main_menu.StartGame.label;
		textComp.fontSize = Language.languageVars.main_menu.StartGame.size;

		textComp = GameObject.Find("Config").GetComponentInChildren<Text>();
		textComp.text     = Language.languageVars.main_menu.Config.label;
		textComp.fontSize = Language.languageVars.main_menu.Config.size;

		textComp = GameObject.Find("About").GetComponentInChildren<Text>();
		textComp.text = Language.languageVars.main_menu.About.label;
		textComp.fontSize = Language.languageVars.main_menu.About.size;

		textComp = GameObject.Find("BestScore").GetComponent<Text>();
		textComp.text     = Language.languageVars.main_menu.BestScore.label;
		textComp.fontSize = Language.languageVars.main_menu.BestScore.size;

		GameObject.Find("Copyright Text").GetComponent<Text>().text = "Copyright © Bitnary Studio™ \n" + Language.languageVars.others.Copyright;

		// Config menu //////////////////////////////////////////////////////////////////
		textComp = GameObject.Find("ConfigText").GetComponent<Text>();
		textComp.text     = Language.languageVars.config_menu.Configuration.label;
		textComp.fontSize = Language.languageVars.config_menu.Configuration.size;

		textComp = GameObject.Find("QualityText").GetComponent<Text>();
		textComp.text     = Language.languageVars.config_menu.Quality.label;
		textComp.fontSize = Language.languageVars.config_menu.Quality.size;

		textComp = GameObject.Find("PerformanceText").GetComponent<Text>();
		textComp.text     = Language.languageVars.config_menu.Performance.label;
		textComp.fontSize = Language.languageVars.config_menu.Performance.size;

		textComp = GameObject.Find("BestQualityText").GetComponent<Text>();
		textComp.text     = Language.languageVars.config_menu.BestQuality.label;
		textComp.fontSize = Language.languageVars.config_menu.BestQuality.size;

		textComp = GameObject.Find("LanguageText").GetComponent<Text>();
		textComp.text     = Language.languageVars.config_menu.Language.label;
		textComp.fontSize = Language.languageVars.config_menu.Language.size;

		this.SystemControl.LoadingScreen.transform.FindChild("LoadingText").gameObject.GetComponent<Text>().text = Language.languageVars.others.Loading;

		GameObject.Find("EnglishBtn").GetComponent<Button>().interactable = (Language.languageVars.language != SystemLanguage.English);
		GameObject.Find("PortugueseBtn").GetComponent<Button>().interactable = (Language.languageVars.language != SystemLanguage.Portuguese);
	}

}

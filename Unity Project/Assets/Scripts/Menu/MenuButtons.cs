using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviourGame {

	private AudioSource InOutSound;

	void Start()
	{
		this.InOutSound = GameObject.Find("InOutSound").GetComponent<AudioSource>();
	}

	public void NewGame()
	{
		SystemControl.LoadLevelWithProgress("GameScene");
	}

	public void ConfigIn()
	{
		GameObject.Find("MainPanel").GetComponent<Animator>().SetBool("In", false);
		GameObject.Find("ConfigPanel").GetComponent<Animator>().SetBool("In", true);

		if (! this.InOutSound.isPlaying) this.InOutSound.Play();
	}
	public void ConfigOut()
	{
		GameObject.Find("ConfigPanel").GetComponent<Animator>().SetBool("In", false);
		GameObject.Find("MainPanel").GetComponent<Animator>().SetBool("In", true);

		if (! this.InOutSound.isPlaying) this.InOutSound.Play();
	}

	public void AboutIn()
	{
		GameObject.Find("MainPanel").GetComponent<Animator>().SetBool("In", false);
		GameObject.Find("AboutPanel").GetComponent<Animator>().SetBool("In", true);

		if (! this.InOutSound.isPlaying) this.InOutSound.Play();
	}

	public void AboutOut()
	{
		GameObject.Find("AboutPanel").GetComponent<Animator>().SetBool("In", false);
		GameObject.Find("MainPanel").GetComponent<Animator>().SetBool("In", true);

		if (! this.InOutSound.isPlaying) this.InOutSound.Play();
	}

	public void setLanguage(string lang)
	{
		PlayerPrefs.SetString("language", lang);
		Language.readLanguage(lang);
		GameObject.Find("Canvas").GetComponent<MainMenu>().setMenuText();
	}

	public void setQuality()
	{
		int quality = (int)GameObject.Find("QualitySlider").GetComponent<Slider>().value;
		PlayerPrefs.SetInt("quality", quality);

		if (quality == 1) QualitySettings.antiAliasing = 2;
		else QualitySettings.antiAliasing = 0; 
	}

}

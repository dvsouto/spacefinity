using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameLocation {Menu, InGame, GameOver};
public enum GameState {Wait, Play, Pause, GameOver};

public class SystemGame : MonoBehaviour {

	public GameLocation gameLocation = new GameLocation();
	public GameState gameState = new GameState();

	public LoadingScreen LoadingScreen;

	// Use this for initialization
	void Awake () {
		GameObject goLoadScreen = GameObject.FindGameObjectWithTag("LoadingScreen");

		if (goLoadScreen != null)
		{
			if (LoadingScreen == null) LoadingScreen = goLoadScreen.GetComponent<LoadingScreen>();
			LoadingScreen.hide();
		}

		string defLang;

		switch(Application.systemLanguage)
		{
			case SystemLanguage.Portuguese: defLang = "pt_BR"; break;
			case SystemLanguage.English: defLang = "en_US"; break;
			default: defLang = "en_US"; break;
		}

		Language.readLanguage(PlayerPrefs.GetString("language", defLang));
	}
	
	// Update is called once per frame
	void Update () {
		switch (gameLocation)
		{
			case GameLocation.InGame: inGame(); break;
		}
	} 

	private void inGame()
	{
		Text TouchToStart = GameObject.Find("TouchToStart").GetComponent<Text>();

		TouchToStart.enabled = (gameState == GameState.Wait);
	}

	public void LoadLevelWithProgress(string levelName)
	{
		StartCoroutine(_LoadLevelWithProgress(levelName));
	}
	
	private IEnumerator _LoadLevelWithProgress(string levelName)
	{
		LoadingScreen.show();

		AsyncOperation ao;
		ao = Application.LoadLevelAsync(levelName);

		while(! ao.isDone)
		{
			int progress = (int)(ao.progress * 100);
			LoadingScreen.setProgress(progress);

			yield return null;
		}
	}

}
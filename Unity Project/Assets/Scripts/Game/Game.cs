using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviourGame {	
	private AsteroidsGeneretor asteroidsGenerator;
	private StarsGenerator starsGenerator;
	private Player player;
	private Background background;
	private GameOverControl gameOverControl;
	private GameObject pauseBtn;

	private bool enableFadeOut = false;
	private AudioSource bgAudioSource;
	public AudioClip gameMusic;

	public int GameQuality {get ; private set;}

	public float gameVelocity = 1;

	public class GameStats
	{
		public float gameTime = 0;
		public int totalAsteroids = 0;
		public int asteroidsDestroyed = 0;

		public void calcTime()
		{
			gameTime += Time.deltaTime;
		}

		public void attStatsScreen()
		{
			Text textGameTime = GameObject.Find("GameTime").GetComponent<Text>();
			Text textTotalAsteroids = GameObject.Find("TotalAsteroids").GetComponent<Text>();
			TimeSpan timeSep = TimeSpan.FromSeconds(gameTime);

			textGameTime.text = string.Format("{0:D2}:{1:D2}", timeSep.Minutes, timeSep.Seconds);
			textTotalAsteroids.text = asteroidsDestroyed.ToString();
		}

		public void resetStats()
		{
			gameTime = 0;
			totalAsteroids = 0;
			asteroidsDestroyed = 0;
		}
	}

	public GameStats gameStats = new GameStats();
	
	protected override void Awake()
	{
		base.Awake();

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		asteroidsGenerator = GameObject.Find("AsteroidGenerator").gameObject.GetComponentInChildren<AsteroidsGeneretor>();
		starsGenerator     = GameObject.Find("StarsGenerator").gameObject.GetComponent<StarsGenerator>();
		background = GameObject.Find("Background").gameObject.GetComponent<Background>();
		bgAudioSource = this.GetComponent<AudioSource>();
		gameOverControl = this.getGameOverControl();
		pauseBtn = GameObject.Find("PauseBtn");

		gameOverControl.hide(false);
	}

	// Use this for initialization
	void Start () {
		if (gameMusic != null) bgAudioSource.clip = gameMusic;
		this.GameQuality = PlayerPrefs.GetInt("quality", 1);

		GameObject.Find("AirplaneGlow").SetActive(GameQuality == 1);

		// Desativar escurecimento da tela
		Screen.sleepTimeout = SleepTimeout.NeverSleep;

		// Carregar idioma ///////////////////////////////////////////

		// Touch to Start
		String touchToStartText = Language.languageVars.others.TouchStart;
		touchToStartText = touchToStartText.Replace("\\n", "\n");
		GameObject.Find("TouchToStart").GetComponent<Text>().text = touchToStartText;
	}
	
	// Update is called once per frame
	void Update () {
		switch (SystemControl.gameState)
		{
			case GameState.Wait: waitTouch(); break;
			case GameState.Play: 
				player.controlPlayer(); 
				background.moveBackground();
				
				gameStats.calcTime();
				gameStats.attStatsScreen();	
				
				if (! bgAudioSource.isPlaying) bgAudioSource.Play();
				//GameObject.Find("GameTime").GetComponent<Text>().text = "Time: " + Math.Floor(gameStats.gameTime) + "segs  |  Asteroids: " + gameStats.asteroidsDestroyed;
			break;
		}

		if (enableFadeOut) fadeOutMusic();
	}

	public void waitTouch()
	{
		pauseBtn.SetActive(false);

		if (Input.touchCount > 0 || Input.GetMouseButton(0))
		{
			SystemControl.gameState = GameState.Play;		
			asteroidsGenerator.Generate = true;
			if (GameQuality == 1) starsGenerator.Generate = true;

			enableFadeOut = false;
			bgAudioSource.time = 0;
			bgAudioSource.volume = 0.65f;

			GameObject.Find("ButtonClickSound").GetComponent<AudioSource>().Play();

			// Exibir botao de pausar
			pauseBtn.SetActive(true);
		}
	}

	public void gameOver()
	{
		// Alterar estado para Game Over
		SystemControl.gameState = GameState.GameOver;

		// Pausar musica
		fadeOutMusic();
		// bgAudioSource.Stop();

		// Parar gerador de asteroides
		asteroidsGenerator.Generate = false;
		asteroidsGenerator.PauseDust();
		if (GameQuality == 1) starsGenerator.Generate = false;
		if (GameQuality == 1) starsGenerator.PauseStars();

		// Exibir painel de game over
		gameOverControl.setScore(gameStats.asteroidsDestroyed);
		gameOverControl.show();

		// Esconder botao de pausar
		pauseBtn.SetActive(false);
	}

	public void restartGame()
	{
		// Reposicionar jogador
		player.transform.position = new Vector3(0, -3.38f, 0);
		player.transform.eulerAngles = new Vector3(0, 0, 0);

		// Destruir asteroides
		asteroidsGenerator.DestroyAll();
		if (GameQuality == 1) starsGenerator.DestroyAll();

		// Resetar estatisticas
		gameStats.resetStats();

		// Resetar posiçao do background
		background.resetPosition();

		// Esconder game over
		gameOverControl.hide();

		// Colocar em modo de espera
		this.SystemControl.gameState = GameState.Wait;
	}

	private void fadeOutMusic()
	{
		bgAudioSource.volume -= (0.5f * Time.deltaTime);

		if (bgAudioSource.volume <= 0) 
		{
			enableFadeOut = false;
			bgAudioSource.Stop();
		} else enableFadeOut = true;
	}

}

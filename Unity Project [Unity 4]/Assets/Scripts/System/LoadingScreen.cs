using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour {

	public GameObject background;
	public GameObject loadingText;
	public GameObject loadingProgress;
	public GameObject asteroidImage;

	private float velAsteroidRotation = 11f;
	private bool colorDirection = false;
	private Color maxColor = Color.white;
	private Color minColor = new Color(0.67f, 0.67f, 0.67f);
	private float timeToColor = 2f;
	private float currentTimeToColor;

	private int progress = 0;

	void Start()
	{
		if (background == null) GameObject.Find ("LoadingBackground");
		if (loadingText == null) GameObject.Find ("LoadingText");
		if (loadingProgress == null) GameObject.Find ("LoadingProgress");
		if (asteroidImage == null) GameObject.Find("AsteroidImage");
	}

	void Update()
	{
		// Atualizar progresso
		loadingProgress.GetComponent<Text>().text = progress + "%";

		// Rotacionar asteroide
		asteroidImage.transform.Rotate( (Vector3.back * velAsteroidRotation) * Time.deltaTime);

		// Piscar estrelas
		currentTimeToColor += Time.deltaTime;
		if (currentTimeToColor >= timeToColor)
		{
			Image backgroundImg = background.GetComponent<Image>();

			colorDirection = ! colorDirection;
		
			if (colorDirection)	backgroundImg.CrossFadeColor(minColor, timeToColor - 0.25f, false, false);
			else                backgroundImg.CrossFadeColor(maxColor, timeToColor - 0.25f, false, false);
			
			currentTimeToColor = 0;
		}
	}	

	public void hide()
	{
		this.gameObject.SetActive(false);
	}

	public void show()
	{
		this.gameObject.SetActive(true);
	}

	public void setProgress(int progress)
	{
		this.progress = progress;
	}
}

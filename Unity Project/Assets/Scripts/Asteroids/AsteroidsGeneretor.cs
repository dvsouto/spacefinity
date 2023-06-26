using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidsGeneretor : MonoBehaviourGame {

	private Game game;
	public GameObject Asteroid;
	public bool Generate = false;

	[System.Serializable]
	public class AsteroidPropertys
	{
		public int maxAsteroids = 3; // Maximo de asteroides gerados

		// Velocidade dos asteroides
		public float minVelocity = 1.2f;
		public float maxVelocity = 2.1f;
		public float addVelocity = 0.08f;

		// Tamanho dos asteroids
		public float minScale = 0.1f;
		public float maxScale = 0.4f;

		// Tempo a serem gerados
		public float timeToGenerate = 3f;
	}
	
	private float currentTimeToGenerate = 0;
	private float positionGenerate = 3f;
	private int asteroidNumber = 1;
	private float subTime = 0.02f;

	public List<GameObject> Asteroids;

	public AsteroidPropertys asteroidProperty = new AsteroidPropertys();
	public AsteroidPropertys defaultAsteroidProperty = new AsteroidPropertys();

	// Use this for initialization
	void Start () {
		Asteroids = new List<GameObject>();
		game = GameObject.Find("GameControl").GetComponent<Game>();

		// Valores padrao para quando reiniciar o jogo
		defaultAsteroidProperty.minVelocity = asteroidProperty.minVelocity;
		defaultAsteroidProperty.maxVelocity = asteroidProperty.maxVelocity;
		defaultAsteroidProperty.addVelocity = asteroidProperty.addVelocity;
		defaultAsteroidProperty.minScale = asteroidProperty.minScale;
		defaultAsteroidProperty.maxScale = asteroidProperty.maxScale;
		defaultAsteroidProperty.timeToGenerate = asteroidProperty.timeToGenerate;
		// Asteroids = new GameObject[maxAsteroids];
	}
	
	// Update is called once per frame
	void Update () {
		if (Generate && Asteroids.Count < asteroidProperty.maxAsteroids)
		{
			currentTimeToGenerate += Time.deltaTime *this.getGameVelocity();

			if (currentTimeToGenerate >= asteroidProperty.timeToGenerate)
			{
				currentTimeToGenerate = 0;

				GameObject newAsteroid;
				AsteroidObject asteroidObject;

				float asteroidScale = Random.Range(asteroidProperty.minScale, asteroidProperty.maxScale);
				float asteroidVelocity = Random.Range(asteroidProperty.minVelocity, asteroidProperty.maxVelocity);
				float asteroidPositionX = Random.Range(-positionGenerate, positionGenerate);

				newAsteroid = GameObject.Instantiate(Asteroid, new Vector3(asteroidPositionX, this.transform.position.y, this.transform.position.z), this.transform.rotation) as GameObject;
				newAsteroid.transform.localScale = new Vector3(asteroidScale, asteroidScale, asteroidScale);

				asteroidObject = newAsteroid.GetComponent<AsteroidObject>();
				asteroidObject.AsteroidNumber = asteroidNumber;
				asteroidObject.Velocity = asteroidVelocity * this.getGameVelocity();
				asteroidObject.generateInternalRotation();
				asteroidObject.Move = true;

				if (asteroidNumber > 50)
				{
					asteroidObject.rotateDirection = true;
					asteroidObject.RotationVel = Random.Range(0f, 5f);

					float randSide = Random.Range(0f, 1f);
					while(randSide == 0.5f) randSide = Random.Range(0f, 1f);

					asteroidObject.rotateSide = (randSide < 0.5f);
				}

				Asteroids.Add (newAsteroid);
				asteroidNumber++;
				game.gameStats.totalAsteroids++;

				asteroidProperty.minVelocity += (asteroidProperty.addVelocity * Time.deltaTime) * this.getGameVelocity();
				asteroidProperty.maxVelocity += (asteroidProperty.addVelocity * Time.deltaTime) * this.getGameVelocity();
				if (asteroidProperty.timeToGenerate > 0.5f) asteroidProperty.timeToGenerate -= subTime * this.getGameVelocity();

				if (asteroidProperty.timeToGenerate <= 3   && asteroidProperty.timeToGenerate >= 2.75f) asteroidProperty.maxAsteroids = 3;
				if (asteroidProperty.timeToGenerate < 2.75f && asteroidProperty.timeToGenerate >= 2.5f) asteroidProperty.maxAsteroids = 4;
				if (asteroidProperty.timeToGenerate < 2.5f && asteroidProperty.timeToGenerate >= 2.25f) asteroidProperty.maxAsteroids = 6;
				if (asteroidProperty.timeToGenerate < 2.25f   && asteroidProperty.timeToGenerate >= 2f) asteroidProperty.maxAsteroids = 8;
				// if (asteroidProperty.timeToGenerate < 2f)                                               asteroidProperty.maxAsteroids = 12;
			}
		}
	}

	// Destruir asteroide por numero
	public void DestroyAsteroid(int asteroidNumber)
	{
		AsteroidObject AstObj;
		for (int i = 0; i < Asteroids.Count-1; i++)
		{
			AstObj = Asteroids[i].GetComponentInChildren<AsteroidObject>();

			if (AstObj.AsteroidNumber == asteroidNumber)
			{
				GameObject.Destroy(Asteroids[i]);
				Asteroids.RemoveAt(i);
				game.gameStats.asteroidsDestroyed++;
			}
		}
	}

	// Destruir todos os asteroides
	public void DestroyAll()
	{
		// Destruir objetos
		Asteroids.ForEach(delegate(GameObject astObj)
		{
			GameObject.Destroy(astObj);
		});

		// Limpar lista
		Asteroids.Clear();

		// Resetar configuraçoes de velocidade
		// Valores padrao para quando reiniciar o jogo
		asteroidProperty.minVelocity    = defaultAsteroidProperty.minVelocity;
		asteroidProperty.maxVelocity    = defaultAsteroidProperty.maxVelocity;
		asteroidProperty.addVelocity    = defaultAsteroidProperty.addVelocity;
		asteroidProperty.minScale       = defaultAsteroidProperty.minScale;
		asteroidProperty.maxScale       = defaultAsteroidProperty.maxScale;
		asteroidProperty.timeToGenerate = defaultAsteroidProperty.timeToGenerate;
		this.asteroidNumber = 1;
		this.currentTimeToGenerate = 0;
	}

	// Pausar poeira dos asteroides
	public void PauseDust()
	{
		if (this.getGameControl().GameQuality == 1)
		{
			// Destruir objetos
			Asteroids.ForEach(delegate(GameObject astObj)
			{
				astObj.GetComponentInChildren<ParticleSystem>().Pause();
			});
		}
	}
}

using UnityEngine;
using System.Collections;

public class AsteroidObject : MonoBehaviourGame {

	public int AsteroidNumber = 0;

	public float Velocity = 1;
	public float RotationVel = 0;
	public float internalRotationVel = 20;

	private float rotateExternalVel = 1;
	public bool rotateDirection = false;
	public bool rotateSide = false;

	public bool Move = false;
	private GameObject AsteroidInner;
	private float destroyAfter = -5.5f;

	// Use this for initialization
	void Start () {
		AsteroidInner = transform.Find("AsteroidTexture").gameObject;

		// Desativar dust caso a qualidade seja 0
		if (this.getGameControl().GameQuality == 0)
			this.transform.FindChild("DustStorm").gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Move)
		{
			this.transform.Translate((-Vector3.up*Velocity) * Time.deltaTime);
			if (rotateDirection)
			{
				Vector3 side;
				if (rotateSide == false) side = Vector3.back; else side = -Vector3.back;

				this.transform.Rotate( ((side*rotateExternalVel) * Time.deltaTime) * this.getGameVelocity());
			}

			AsteroidInner.transform.Rotate((Vector3.back*internalRotationVel) * Time.deltaTime);

			// Alto-destruiçao
			if (this.transform.position.y <= destroyAfter)
			{
				this.Move = false;
				GameObject.Find("AsteroidGenerator").gameObject.GetComponentInChildren<AsteroidsGeneretor>().DestroyAsteroid(this.AsteroidNumber);
				//Destroy(this.gameObject);
			}

			// Parar asteroide caso de game over
			if (this.SystemControl.gameState == GameState.GameOver) this.Move = false;
		}
	}

	public void generateInternalRotation()
	{
		// Variaveis que definem o minimo e o maximo da rotaçao interna do asteroide
		float minRot = 8* (Velocity-0.2f);
		float maxRot = 50 * (Velocity-0.2f);

		// Calcular randomicamente entre o valor minimo e maximo a rotaçao interna
		internalRotationVel = Random.Range(minRot, maxRot);

		// Variavel que ira definir se ira rotacionar a esquerda ou a direita
		float sideRotation = 0;
		while (sideRotation == 0)
			sideRotation = Random.Range(-1f, 1f);

		// Definir lado da rotaçao
		if (sideRotation < 0) internalRotationVel = -internalRotationVel;
	}

}

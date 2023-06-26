using UnityEngine;
using System.Collections;

public class Star : MonoBehaviourGame {

	public bool move = true;

	private float velocity = 1.1f;
	private float destroyAfter = -5.5f;

	private Color[] randomColors;

	void Start()
	{
		ParticleSystem[] ps = this.GetComponentsInChildren<ParticleSystem>();

		ps[0].startSize = Random.Range(0.3f, 1.4f);
		ps[1].startSize = Random.Range(2f, 4f);
		ps[0].emissionRate = Random.Range(25, 60);
		ps[1].emissionRate = Random.Range(40, 60);

		randomColors = new Color[6];
		randomColors[0] = Color.white;
		randomColors[1] = Color.blue;
		randomColors[2] = Color.cyan;
		randomColors[3] = Color.green;
		randomColors[4] = Color.yellow;
		randomColors[5] = Color.white;

		int startColor = Random.Range(0,randomColors.Length);
		ps[0].startColor = randomColors[startColor];
	}


	// Update is called once per frame
	void Update () {
		float velocityReal = velocity * GameObject.Find("Background").GetComponent<Background>().returnVelBackground();
		if (this.move)
		{
			// Mover estrela
			this.transform.Translate( (-Vector3.up*velocityReal*this.getGameVelocity()) * Time.deltaTime);

			// Parar asteroide caso de game over
			if (this.SystemControl.gameState == GameState.GameOver) this.move = false;
		}

		// Alto-destruiçao
		if (this.transform.position.y <= destroyAfter)
		{
			this.move = false;
			Destroy(this.gameObject);

			//Destroy(this.gameObject);
		}
	}
}

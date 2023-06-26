using UnityEngine;
using System.Collections;

public class Player : MonoBehaviourGame {
	public float velocity = 4.9f;
	
	private float maxMovement = 2.75f;
	private float maxRotation = 25f;
	private float actualRotation = 0;

	private float fixRotation = 50; // 35;
	private float fixMoveRotation = 180; // 200;

	public GameObject ParticleExplosion;
	public AudioClip SoundExplosion;
	
	// Update is called once per frame
	void Update () {
	
	}

	public void controlPlayer()
	{
		float moveX = Input.acceleration.x;
		//moveX = Input.GetAxis("Horizontal"); // DESENVOLVIMENTO !!!
		GameObject Airplane = GameObject.FindGameObjectWithTag("Player");
		
		// Movimentar aviao
		if (checkPosition(Airplane, moveX))
		{
			Airplane.transform.Translate((moveX*velocity) *Time.deltaTime, 0, 0);
			AirplaneRotation(Airplane, moveX);
		}
	}
	
	// Verifica se ultrapassou as margens da tela
	private bool checkPosition(GameObject Airplane, float moveX)
	{
		if (moveX < 0 && Airplane.transform.position.x <= -maxMovement)
		{
			Airplane.transform.position.Set(-maxMovement, Airplane.transform.position.y, Airplane.transform.position.z);
			return false;
		} else
			if (moveX > 0 && Airplane.transform.position.x >= maxMovement) 
		{
			Airplane.transform.position.Set(maxMovement, Airplane.transform.position.y, Airplane.transform.position.z);
			return false;
		} else return true;
	}
	
	private void AirplaneRotation(GameObject Airplane, float toDirection)
	{
		if (toDirection < -0.06f || toDirection > 0.06f)
			actualRotation += (toDirection*fixMoveRotation) * Time.deltaTime;
		else 
		{
			if (actualRotation > 0) actualRotation -= (fixRotation*Random.Range(0.75f, 1)) * Time.deltaTime;
			if (actualRotation < 0) actualRotation += (fixRotation*Random.Range(0.75f, 1)) * Time.deltaTime;
		}
		
		
		//Debug.Log(toDirection);
		if (actualRotation < -maxRotation) actualRotation = -maxRotation;
		if (actualRotation >  maxRotation) actualRotation =  maxRotation;
		
		Airplane.transform.eulerAngles = new Vector3(0,-actualRotation,0);
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (ParticleExplosion != null) Instantiate(ParticleExplosion, other.transform.position, new Quaternion());
		if (SoundExplosion != null)
		{
			AudioSource airplaneAudio = this.GetComponent<AudioSource>();
			float audioPitch = Random.Range(0.8f, 1.2f);

			airplaneAudio.clip = SoundExplosion;
			airplaneAudio.pitch = audioPitch;
			airplaneAudio.Play();
		}


		this.getGameControl().gameOver();
	}
}

using UnityEngine;
using System.Collections;

public class Background : MonoBehaviourGame {

	private GameObject background;
	private float offsetBackground = 0;
	private float velMoveBackground = -0.024f; //-0.013f;
	private float addMoveVelocity = 0.00008f;

	private float maxOffsetX = 0.10f;
	private float moveXVel = 0.009f;


	private float defaultVelMoveBackground;

	void Start()
	{
		defaultVelMoveBackground = velMoveBackground;
	}

	public void resetPosition()
	{
		// Resetar offset
		offsetBackground = 0;
		this.gameObject.GetComponent<Renderer>().material.SetTextureOffset("_MainTex", new Vector2(0, 0));

		// Resetar velocidade
		velMoveBackground = defaultVelMoveBackground; 
	}

	public void moveBackground()
	{
		Material matBackground = this.gameObject.GetComponent<Renderer>().material;
		Vector2 actualOffsetBackground = matBackground.GetTextureOffset("_MainTex");

		// Movimento Y continuo
		offsetBackground += (velMoveBackground * Time.deltaTime) * this.getGameVelocity();
		matBackground.SetTextureOffset("_MainTex", new Vector2(actualOffsetBackground.x, offsetBackground));
		if (velMoveBackground > -0.12f) velMoveBackground -= addMoveVelocity * Time.deltaTime; // Simular aumento de velocidade

		// Movimento X de acordo com aviao
		float toDirection = Input.acceleration.x;
		bool canMove = false;

		if (toDirection < -0.06f || toDirection > 0.06f)
		{
			float offsetX = actualOffsetBackground.x;

			if (toDirection < -0.06f)
			{
				if (offsetX < maxOffsetX) canMove = true;
				else 
				{
					canMove = false;
					if (offsetX < maxOffsetX) matBackground.SetTextureOffset("_MainTex", new Vector2(maxOffsetX, offsetBackground));
				}
			} else
			if (toDirection > 0.06f)
			{
				if (offsetX > -maxOffsetX) canMove = true;
				else
				{
					canMove = false;
					if (offsetX > -maxOffsetX) matBackground.SetTextureOffset("_MainTex", new Vector2(-maxOffsetX, offsetBackground));
				}
			}

			if (canMove) 
			{
				float newX = actualOffsetBackground.x;
				newX += (-toDirection * moveXVel) * Time.deltaTime;

				matBackground.SetTextureOffset("_MainTex", new Vector2(newX, offsetBackground));
			}
		}
	}

	public float returnVelBackground()
	{
		return -velMoveBackground*50;
	}
}

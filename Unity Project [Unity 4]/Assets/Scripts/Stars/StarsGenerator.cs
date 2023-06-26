using UnityEngine;
using System.Collections;

public class StarsGenerator : MonoBehaviour {

	public bool Generate = true;
	                                            
	// Tempo a serem gerados
	private float timeToGenerate = 3f;
	private float currentTimeToGenerate = 0;
	private float positionGenerate = 3f;

	public GameObject Star;

	// Use this for initialization
	void Start () {
		this.timeToGenerate = Random.Range(8f, 22f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Generate)
		{
			currentTimeToGenerate += Time.deltaTime;

			if (currentTimeToGenerate >= this.timeToGenerate)
			{
				currentTimeToGenerate = 0;
				this.timeToGenerate = Random.Range(8f, 22f);

				float starPositionX = Random.Range(-positionGenerate, positionGenerate);
				Instantiate(Star, new Vector3(starPositionX, this.transform.position.y, this.transform.position.z), this.transform.rotation);
			}
		}
	}

	public void PauseStars()
	{
		GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
		
		for(int i = 0; i < stars.Length ; i++)
		{
			ParticleSystem[] ps = stars[i].GetComponentsInChildren<ParticleSystem>();

			for(int p = 0; p < ps.Length; p++)
				ps[p].Pause();
		}
	}
	
	public void DestroyAll()
	{
		GameObject[] stars = GameObject.FindGameObjectsWithTag("Star");
		
		for(int i = 0; i < stars.Length ; i++)
			Destroy (stars[i]);
	}
}

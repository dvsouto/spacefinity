using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public bool EnableScript = true;
	public float timeToDestroy = 3f;

	private float currentTime = 0;

	// Update is called once per frame
	void Update () {
		if (EnableScript)
		{
			currentTime += Time.deltaTime;

			if (currentTime >= timeToDestroy)
				Destroy(this.gameObject);
		}
	}
}

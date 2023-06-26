using UnityEngine;
using System.Collections;

public class MonoBehaviourGame : MonoBehaviour {

	protected SystemGame SystemControl;

	// Use this for initialization
	protected virtual void Awake () {
		SystemControl = GameObject.FindGameObjectWithTag("SystemControl").GetComponent<SystemGame>();
	}

	public Game getGameControl()
	{
		if (GameObject.FindGameObjectWithTag("GameControl") != null)
			return GameObject.FindGameObjectWithTag("GameControl").GetComponent<Game>();
		else return null;
	}

	public GameObject getObjGameControl()
	{
		return GameObject.FindGameObjectWithTag("GameControl");
	}

	public GameOverControl getGameOverControl()
	{	if (GameObject.FindGameObjectWithTag("GameOverControl") != null)
			return GameObject.FindGameObjectWithTag("GameOverControl").GetComponent<GameOverControl>();
		else return null;
	}
	
	public GameObject getObjGameOverControl()
	{
		return GameObject.FindGameObjectWithTag("GameOverControl");
	}

	protected float getGameVelocity()
	{
		return GameObject.Find("GameControl").GetComponent<Game>().gameVelocity;
	}
}

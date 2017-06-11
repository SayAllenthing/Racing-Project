using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersManager : MonoBehaviour {

	public static PlayersManager Instance;

	public List<PlayerData> Players;

	// Use this for initialization
	void Start () 
	{
		if(Instance != null)
			Destroy(Instance.gameObject);

		Instance = this;

		foreach(Transform t in transform)
		{
			Players.Add(t.GetComponent<PlayerData>());
		}

		DontDestroyOnLoad(this.gameObject);
	}

	public List<PlayerData> GetActivePlayers()
	{
		List<PlayerData> ret = new List<PlayerData>();

		foreach(PlayerData p in Players)
		{
			if(p.bIsActive)
				ret.Add(p);
		}

		return ret;
	}

}

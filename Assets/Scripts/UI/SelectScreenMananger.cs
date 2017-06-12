using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectScreenMananger : MonoBehaviour {

	public List<PlayerData> ActivePlayers = new List<PlayerData>();
	public List<PlayerData> ReadyPlayers = new List<PlayerData>();

	bool Ready = false;

	public void ActivatePlayer(PlayerData p)
	{
		ActivePlayers.Add(p);
	}

	public void UnactivatePlayer(PlayerData p)
	{
		ActivePlayers.Remove(p);
	}

	public void ReadyPlayer(PlayerData p)
	{
		ReadyPlayers.Add(p);
	}

	public void UnreadyPlayer(PlayerData p)
	{
		ReadyPlayers.Remove(p);
	}


	void Update()
	{
		if(Ready)
			return;

		if(ReadyPlayers.Count > 0 && (ActivePlayers.Count == ReadyPlayers.Count))
		{
			Ready = true;
			StartGame();
		}
	}

	void StartGame()
	{
		foreach(PlayerData d in ReadyPlayers)
		{
			d.bIsActive = true;
		}

		UnityEngine.SceneManagement.SceneManager.LoadScene("TrackFarm");
	}
}

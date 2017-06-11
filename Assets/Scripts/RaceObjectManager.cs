using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceObjectManager : MonoBehaviour {

	public RaceManager raceManager;

	public GameObject CameraPrefab;

	int playerCount = 0;

	public void SpawnPlayers()
	{
		List<PlayerData> list = PlayersManager.Instance.GetActivePlayers();
		playerCount = list.Count;

		for(int i = 0; i < list.Count; i++)
		{
			GameObject prefab = CarPrefabManager.Instance.Cars[list[i].CarPrefab];
			GameObject g = Instantiate(prefab, raceManager.StartingPoints[i].position + Vector3.up * 2, Quaternion.identity);
			PlayerController pc = g.GetComponent<PlayerController>();
			pc.Init();
			pc.pig.SetHat(list[i].HatPrefab);
			pc.Number = list[i].Number;

			GameObject cam = Instantiate(CameraPrefab, g.transform.position, Quaternion.identity);
			cam.GetComponent<CameraController>().SetTarget(g.transform);

			pc.cam = cam.GetComponent<CameraController>();

			SetCanvas(list[i], cam.GetComponent<CameraController>().Panel);
		}

		SpawnAI();
	}

	void SetCanvas(PlayerData data, RectTransform t)
	{
		if(playerCount == 1)
		{
			return;
		}
	}

	void SpawnAI()
	{
		for(int i = playerCount; i < 6; i++)
		{
			GameObject prefab = CarPrefabManager.Instance.Cars[Random.Range(0,3)];
			GameObject g = Instantiate(prefab, raceManager.StartingPoints[i].position + Vector3.up * 2, Quaternion.identity);
			g.GetComponent<AIController>().Init();
		}
	}

}

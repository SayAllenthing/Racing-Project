using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceObjectManager : MonoBehaviour {

	public RaceManager raceManager;

	public GameObject CameraPrefab;

	int playerCount = 0;

    List<Transform> StartingPointsJumbled;

    public bool bSpawnAI = false;

    private void Start()
    {
        StartingPointsJumbled = new List<Transform>(raceManager.StartingPoints);
        for (int i = 0; i < StartingPointsJumbled.Count; i++)
        {
            Transform temp = StartingPointsJumbled[i];
            int randomIndex = Random.Range(i, StartingPointsJumbled.Count);
            StartingPointsJumbled[i] = StartingPointsJumbled[randomIndex];
            StartingPointsJumbled[randomIndex] = temp;
        }
    }

    public void SpawnPlayers()
	{
		List<PlayerData> list = PlayersManager.Instance.GetActivePlayers();
		playerCount = list.Count;

		for(int i = 0; i < list.Count; i++)
		{
			GameObject prefab = CarPrefabManager.Instance.Cars[list[i].CarPrefab];
			GameObject g = Instantiate(prefab, StartingPointsJumbled[i].position + Vector3.up * 2, Quaternion.identity);
			PlayerController pc = g.GetComponent<PlayerController>();
			pc.Init();
			pc.pig.SetHat(list[i].HatPrefab);
			pc.Number = list[i].Number;

			GameObject cam = Instantiate(CameraPrefab, g.transform.position, Quaternion.identity);
			cam.GetComponent<CameraController>().SetTarget(g.transform);

			pc.cam = cam.GetComponent<CameraController>();

			SetCanvas(list[i], cam.GetComponent<CameraController>());
		}

		SpawnAI();
	}

	void SetCanvas(PlayerData data, CameraController c)
	{
        if (playerCount == 1)
        {
            return;
        }

        List<PlayerData> list = PlayersManager.Instance.GetActivePlayers();

        Camera cam = c.cam;
        RectTransform t = c.Panel;

        if (playerCount == 2)
        {
            PlayerData Lowest = (int)list[0].Number < (int)list[1].Number ? list[0] : list[1];
            
            //Am I top?
            if(data == Lowest)
            {
                cam.rect = new Rect(0, 0.5f, 1, 0.5f);
                t.localScale = new Vector3(1, 0.5f, 1);
            }
            else
            {
                cam.rect = new Rect(0, 0, 1, 0.5f);
                t.localScale = new Vector3(1, 0.5f, 1);

                t.anchoredPosition = new Vector2(0, -360);
            }

            return;
        }

        //4 sections
        switch (data.Number)
        {
            case PlayerNumber.One:
                cam.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
                t.localScale = new Vector3(0.5f, 0.5f, 1);
                break;
            case PlayerNumber.Two:
                cam.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                t.localScale = new Vector3(0.5f, 0.5f, 1);
                t.anchoredPosition = new Vector2(640, 0);
                break;
            case PlayerNumber.Three:
                cam.rect = new Rect(0f, 0f, 0.5f, 0.5f);
                t.localScale = new Vector3(0.5f, 0.5f, 1);
                t.anchoredPosition = new Vector2(0, -360);
                break;
            case PlayerNumber.Four:
                cam.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
                t.localScale = new Vector3(0.5f, 0.5f, 1);
                t.anchoredPosition = new Vector2(640, -360);
                break;
        }

        //Set Unused camera
        if(playerCount == 3)
        {
            int num = 0;
            for(int i = 0; i < 4; i++)
            {
                bool found = false;
                foreach(PlayerData p in list)
                {
                    if ((int)p.Number == i)
                    {
                        found = true;
                        break;                        
                    }
                }

                if (!found)
                {
                    num = i+1; 
                    break;
                }
            }

            Camera main = Camera.main;

            switch (num)
            {
                case 1:
                    main.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);
                    break;
                case 2:
                    main.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                    break;
                case 3:
                    main.rect = new Rect(0f, 0f, 0.5f, 0.5f);
                    break;
                case 4:
                    main.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
                    break;
            }
        }

	}

	void SpawnAI()
	{
        if (!bSpawnAI)
            return;

		for(int i = playerCount; i < 6; i++)
		{
			GameObject prefab = CarPrefabManager.Instance.Cars[Random.Range(0,3)];
			GameObject g = Instantiate(prefab, StartingPointsJumbled[i].position + Vector3.up * 2, Quaternion.identity);
			g.GetComponent<AIController>().Init();
		}
	}

}

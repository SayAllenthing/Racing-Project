using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostGameManager : MonoBehaviour
{
    public List<Transform> Positions = new List<Transform>();

    public List<GameObject> Cars;

	public AudioSource EndingMusic;

    float EndTimer = -1;

    private void Start()
    {
        DontDestroyOnLoad(this);
    }

	public void OnRaceComplete()
	{
		EndingMusic.Play();
	}

    public void StartPostGame(List<CarController> Places)
    {
        for(int i = 0; i < Places.Count; i++)
        {
            Cars.Add(Places[i].gameObject);
            Places[i].transform.parent = transform;
        }
    }

    void PopulatePositions()
    {
        GameObject g = GameObject.Find("Positions");
        for (int i = 0; i < g.transform.childCount; i++)
        {
            Positions.Add(g.transform.GetChild(i).transform);
        }
    }

    void PositionCars()
    {
        for (int i = 0; i < Positions.Count; i++)
        {
            Cars[i].transform.SetParent(Positions[i]);
            Cars[i].transform.localPosition = Vector3.zero;
            Cars[i].transform.localRotation = Quaternion.identity;
            Cars[i].transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void StartEndTimer()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
		if(EndTimer > 0)
        {
            EndTimer -= Time.deltaTime;
            if(EndTimer <= 0)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
                DestroyImmediate(gameObject);
            }
        }
	}

    private void OnLevelWasLoaded(int level)
    {
        if(level == 4)
        {
            PopulatePositions();
            PositionCars();
            EndTimer = 15;

			GameObject.Find("AwardManager").GetComponent<AwardManager>().Init(Cars);
        }
    }
}

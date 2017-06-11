using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceManager : MonoBehaviour {

	public List<CarController> Cars;

	public List<CarController> Places = new List<CarController>();
	public List<CarController> Finished = new List<CarController>();

	public List<Collider> CheckPoints;

	public List<Transform> StartingPoints;

	public RaceObjectManager ObjectManager;


	public int RaceLength = 0;

	public int Laps = 2;

	float UpdatePlacesTimer = 0;

	public bool bRaceComplete = false;

	float EndTimer = -1;

	// Use this for initialization
	void Start () 
	{
		RaceLength = CheckPoints.Count;

		ObjectManager.SpawnPlayers();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Time.time > UpdatePlacesTimer)
		{
			UpdatePlaces();
		}

		if(EndTimer > 0 && Time.time > EndTimer)
		{
			EndRace();
		}
	}

	public void AddCar(CarController car)
	{
		Cars.Add(car);
		UpdatePlaces();
	}

	public Collider GetNextCheckPoint(CarController car)
	{
		if(car.LapProgess == CheckPoints.Count)
		{			
			car.LapProgess = 0;
			if(car.GetLap() + 1 > Laps)
			{
				if(!bRaceComplete)
				{
					//Race is done
					bRaceComplete = true;
					EndTimer = Time.time + 10;
				}
			}
			car.NewLap();
		}

		return CheckPoints[car.LapProgess];
	}

	void UpdatePlaces()
	{
		Places.Clear();

		List<CarController> c = new List<CarController>(Cars);

		for(int i = 0; i < Finished.Count; i++)
		{
			Places.Add(Finished[i]);
			c.Remove(Finished[i]);
		}

		while(c.Count > 0)
		{
			CarController toAdd = null;
			int highest = -1;

			foreach(CarController car in c)
			{
				int score = car.LapProgess + car.GetLap() * CheckPoints.Count;
				if(score > highest)
				{
					toAdd = car;
					highest = score;
				}
				else if(score == highest)
				{
					if(toAdd.GetDistance() > car.GetDistance())
					{
						toAdd = car;
					}
				}
			}

			c.Remove(toAdd);
			Places.Add(toAdd);
		}



		UpdatePlacesTimer = Time.time + 0.5f;
	}

	public int GetPlace(CarController car)
	{
		for(int i = 0; i < Places.Count; i++)
		{
			if(car == Places[i])
				return i+1;
		}

		return -1;
	}

	public int Finish(CarController car)
	{
		Finished.Add(car);
		return Finished.Count;
	}

	void EndRace()
	{
		EndTimer = -1;
		UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
	}
}

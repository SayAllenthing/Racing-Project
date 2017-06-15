using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwardManager : MonoBehaviour {

	enum AwardType
	{
		HighFlyer,
		SpeedDemon,
		Gassy,
		LadyLike,
		Popcorn,
		Horny,
		BestDressed,
		BiggestFailure
	}

	public GameObject AwardPrefab;

	public List<Tracker> Trackers;
	public List<Transform> AwardContainers = new List<Transform>();

	int[] NumAwards;

	public void Init(List<GameObject> cars)
	{
		NumAwards = new int[6];

		for(int i = 0; i < cars.Count; i++)
		{
			Trackers.Add(cars[i].GetComponent<Tracker>());

		}

		GiveAward(AwardType.HighFlyer);
		GiveAward(AwardType.SpeedDemon);
		GiveAward(AwardType.Gassy);
		GiveAward(AwardType.LadyLike);
		GiveAward(AwardType.Popcorn);
		GiveAward(AwardType.Horny);
		GiveAward(AwardType.BestDressed);
		GiveAward(AwardType.BiggestFailure);
	}
	
	void GiveAward(AwardType award)
	{
		switch (award) 
		{
		case AwardType.HighFlyer:
			GiveHighFlyer();
			break;
		case AwardType.SpeedDemon:
			GiveSpeedDemon();
			break;
		case AwardType.Gassy:
			GiveGassy();
			break;
		case AwardType.LadyLike:
			GiveLadyLike();
			break;
		case AwardType.Popcorn:
			GivePopcorn();
			break;
		case AwardType.Horny:
			GiveHorny();
			break;
		case AwardType.BestDressed:
			GiveBestDressed();
			break;
		case AwardType.BiggestFailure:
			GiveBiggestFailure();
			break;
		}
	}

	void GiveAward(string title, string desc, int index)
	{
		GameObject g = Instantiate(AwardPrefab, AwardContainers[index]);
		g.GetComponent<Award>().Init(title, desc);

		NumAwards[index]++;
	}

	void GiveHighFlyer()
	{
		float highest = 0;
		int index = -1;

		for(int i = 0; i < Trackers.Count; i++)
		{
			if(Trackers[i].HighestAltitude > highest)
			{
				highest = Trackers[i].HighestAltitude;
				index = i;
			}				
		}

		if(NumAwards[index] < 3)
		{
			string title = "High Flyer";
			string desc = "Reached " + highest.ToString("0.0") + " meters";

			GiveAward(title, desc, index);
		}
	}

	void GiveSpeedDemon()
	{
		float highest = 0;
		int index = -1;

		for(int i = 0; i < Trackers.Count; i++)
		{
			if(Trackers[i].FastestSpeed > highest)
			{
				highest = Trackers[i].FastestSpeed;
				index = i;
			}				
		}

		if(NumAwards[index] < 3)
		{
			string title = "Speed Demon";
			string desc = "Reached " + (highest * 2.5f).ToString("0") + " Km/h";

			GiveAward(title, desc, index);
		}
	}

	void GiveGassy()
	{
		float highest = 0;
		int index = -1;

		for(int i = 0; i < Trackers.Count; i++)
		{
			if(Trackers[i].NumFarts > highest)
			{
				highest = Trackers[i].NumFarts;
				index = i;
			}				
		}

		if(NumAwards[index] < 3)
		{
			string title = "Gassy";
			string desc = "Farted " + highest.ToString("0") + " times";

			GiveAward(title, desc, index);
		}
	}

	void GiveLadyLike()
	{
		float highest = 999999;
		int index = -1;

		for(int i = 0; i < Trackers.Count; i++)
		{
			if(Trackers[i].NumFarts < highest)
			{
				highest = Trackers[i].NumFarts;
				index = i;
			}				
		}

		if(NumAwards[index] < 3)
		{
			string title = "Lady Like";
			string desc = "Farted " + highest.ToString("0") + " times";

			GiveAward(title, desc, index);
		}
	}

	void GivePopcorn()
	{
		float highest = 0;
		int index = -1;

		for(int i = 0; i < Trackers.Count; i++)
		{
			if(Trackers[i].CornHit > highest)
			{
				highest = Trackers[i].CornHit;
				index = i;
			}				
		}

		if(NumAwards[index] < 3)
		{
			string title = "Popcorn";
			string desc = "Destroyed " + highest.ToString("0") + " corn";

			GiveAward(title, desc, index);
		}
	}

	void GiveHorny()
	{
		float highest = 0;
		int index = -1;

		for(int i = 0; i < Trackers.Count; i++)
		{
			if(Trackers[i].HornPressed > highest)
			{
				highest = Trackers[i].HornPressed;
				index = i;
			}				
		}

		if(NumAwards[index] < 3)
		{
			string title = "Horny";
			string desc = highest.ToString("0") + " horn honks";

			GiveAward(title, desc, index);
		}
	}

	void GiveBestDressed()
	{
		int minAwards = 0;
		int index = -1;

		int[] jumbled = new int[] {0,1,2,3,4,5};
		for(int i = 0; i < jumbled.Length; i++)
		{
			int temp = jumbled[i];
			int randomIndex = Random.Range(i, jumbled.Length);
			jumbled[i] = jumbled[randomIndex];
			jumbled[randomIndex] = temp;
		}

		while(index == -1)
		{
			for(int j = 0; j < jumbled.Length; j++)
			{
				if(NumAwards[jumbled[j]] == minAwards)
					index = jumbled[j];
			}

			if(index < 0)
				minAwards++;
		}

		string title = "Best Dressed";
		string desc = "Looking snazzy";

		GiveAward(title, desc, index);
	}

	void GiveBiggestFailure()
	{
		int minAwards = 0;
		int index = -1;

		int[] jumbled = new int[] {0,1,2,3,4,5};
		for(int i = 0; i < jumbled.Length; i++)
		{
			int temp = jumbled[i];
			int randomIndex = Random.Range(i, jumbled.Length);
			jumbled[i] = jumbled[randomIndex];
			jumbled[randomIndex] = temp;
		}

		while(index == -1)
		{
			for(int j = 0; j < jumbled.Length; j++)
			{
				if(NumAwards[jumbled[j]] == minAwards)
					index = jumbled[j];
			}

			if(index < 0)
				minAwards++;
		}

		string title = "Biggest Failure";
		string desc = "C'mon...";

		GiveAward(title, desc, index);
	}
}

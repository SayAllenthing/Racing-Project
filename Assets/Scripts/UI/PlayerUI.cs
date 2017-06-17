using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI: MonoBehaviour {

    public Rigidbody body;
    public TextMeshProUGUI SpeedText;
	public TextMeshProUGUI LapText;
	public TextMeshProUGUI PlaceText;

	public TextMeshProUGUI FinishPlaceText;

    public FartBar fartBar;

	CarController car;
	RaceManager raceManager;

	// Use this for initialization
	public void Init (CarController c) 
	{
		body = c.GetComponent<Rigidbody>();
		car = c;
		raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(car == null)
			return;

		SpeedText.text = (body.velocity.magnitude * 2.5f).ToString("0") + " Km/h";
		LapText.text = "Lap: " + Mathf.Clamp(car.GetLap(), 1, raceManager.Laps)  + "/" + raceManager.Laps;

		PlaceText.text = raceManager.GetPlace(car) + "/6";

        fartBar.SetFillAmount(car.FartAmount/100f);
	}

	CarController GetActiveController()
	{
		CarController[] controllers = body.GetComponents<CarController>();
		foreach(CarController c in controllers)
		{
			if(c.bIsActive)
			{				
				return c;
			}
		}

		return null;
	}

	public void OnRaceComplete()
	{
		int place = raceManager.GetPlace(car);

		FinishPlaceText.enabled = true;
		FinishPlaceText.text = GetPlaceText(place);
	}

	string GetPlaceText(int place)
	{
		switch(place)
		{
		case 1: return "1st"; break;
		case 2: return "2nd"; break;
		case 3: return "3rd"; break;
		}

		return place.ToString() + "th";
	}
}

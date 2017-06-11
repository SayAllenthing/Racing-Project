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

	CarController car;
	RaceManager raceManager;

	// Use this for initialization
	public void Init (Rigidbody b) 
	{
		body = b;
		car = GetActiveController();
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
}

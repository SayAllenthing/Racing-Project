using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

	public bool bIsActive = false;

	public Pig pig;
	protected VehicleMotor motor;
	protected Rigidbody body;

	RaceManager raceManager;

	[HideInInspector]
	public int LapProgess = 0;
	[HideInInspector]
	protected int Lap = 1;

	protected Collider NextCheckPoint = null;

	protected bool CanFlip = false;
	float FlipTime = 0;

	int Place = 0;



	public virtual void Init()
	{		
		motor = GetComponent<VehicleMotor>();
		body = GetComponent<Rigidbody>();

		raceManager = GameObject.Find("RaceManager").GetComponent<RaceManager>();

		NextCheckPoint = raceManager.GetNextCheckPoint(this);
		OnNextCheckPoint();

		if(raceManager != null && !raceManager.bRaceComplete)
		{
			raceManager.AddCar(this);
		}		
	}

    public void Activate()
    {
        bIsActive = true;
    }
	
	// Update is called once per frame
	void Update () 
	{
		if(Vector3.Dot(transform.up, Vector3.up) < 0)
		{
			if(FlipTime > 2)
				CanFlip = true;

			FlipTime += Time.deltaTime;
		}
		else
		{
			FlipTime = 0;
			CanFlip = false;
		}
	}

	public void Flip()
	{
        int index = LapProgess;
        if (index == 0)
            index = raceManager.CheckPoints.Count - 1;

        Transform t = raceManager.CheckPoints[index-1].transform;

		transform.position = t.position + Vector3.up * 5;
		transform.rotation = t.rotation;

		CanFlip = false;
		FlipTime = 0;
	}

	void OnTriggerEnter(Collider c)
	{		
		if(c == NextCheckPoint)
		{
			LapProgess++;
			NextCheckPoint = raceManager.GetNextCheckPoint(this);
			OnNextCheckPoint();
		}
	}

	public void Fart()
	{
		motor.Boost();
		pig.OnFart();
	}

	public void NewLap()
	{
		Lap++;
		LapProgess = 0;
		if(Lap > raceManager.Laps)
			OnRaceComplete();
	}

	public int GetLap()
	{
		return Lap;
	}

	public float GetDistance()
	{
		return Mathf.Abs((NextCheckPoint.transform.position - transform.position).magnitude);
	}

	virtual protected void OnNextCheckPoint()
	{

	}

	virtual protected void OnRaceComplete()
	{
		raceManager.Finished.Add(this);
	}

    public void KillMovement()
    {
        if(motor != null)
        {
            motor.KillSpeed();
            motor.motor = 0;
            motor.steering = 0;
        }

        if (body != null)
        {
            body.velocity = Vector3.zero;
            body.angularVelocity = Vector3.zero;
        }

        bIsActive = false;        
    }
}

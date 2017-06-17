using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : CarController {

	bool TurningAround = false;

	Vector3 WantPosition;

	public bool PlaceAiMarkers;
	public GameObject AiMarkerPrefab;

	float StuckTime = 0;

	float NextFartTime = 0;

	float MaxSpeed = 99;
	bool TryFart = false;

	float NextHornTime = -1;

	public override void Init()
	{
		base.Init();
		NextHornTime = Time.time + Random.Range(1,25);
	}

	void FixedUpdate()
	{
		if(!bIsActive || NextCheckPoint == null)
			return;

		if(CanFlip)
			Flip();

		Vector3 targetDir = WantPosition - transform.position;
		float distance = targetDir.magnitude;
		targetDir = targetDir.normalized;

		float dot = Vector3.Dot(targetDir, transform.forward);
		float angle = Mathf.Acos( dot ) * Mathf.Rad2Deg; 

		float whichWay = -Vector3.Cross(targetDir, transform.forward).y;
		float forward = -Vector3.Cross(NextCheckPoint.transform.forward, transform.forward).y;

		motor.motor = 1;

		//Move and Steer
		if(angle < 100 && !TurningAround)
		{
			motor.steering = whichWay/2 + forward/2;

			//Sharp Turn
			if(Mathf.Abs(motor.steering) > 0.5f)
			{				
				if(body.velocity.magnitude > 12f)
					motor.motor = -1f;
				else if(body.velocity.magnitude < 3)
					motor.motor = 1;
				else
					motor.motor = 0f;
			}
			else if(Mathf.Abs(motor.steering) > 0.25f)//Minor Turn
			{
				if(body.velocity.magnitude > 15)
					motor.motor = -0.5f;
				else if(body.velocity.magnitude < 4)
					motor.motor = 1f;
				else
					motor.motor = 0.5f;
			}
		}
		else
		{
			TurningAround = true;

			motor.motor = -0.75f;
			motor.steering = whichWay > 0 ? -1 : 1;

			//Can I move that way?
			RaycastHit hit;
			int layerMask = 1 << LayerMask.NameToLayer("Fence");
			if(Physics.Raycast(transform.position, -transform.forward, out hit, 1.5f, layerMask))
			{
				motor.motor = 1;
			}

			if(angle < 30)
				TurningAround = false;
		}

		//Am I going to fast for the checkpoint
		if(body.velocity.magnitude > MaxSpeed)
		{
			motor.motor = -1;
		}

		//Am I stuck Try to get Unstuck
		if(body.velocity.magnitude < 1.4f && Mathf.Abs(motor.motor) > 0.5f)
		{
			RaycastHit hit;
			int layerMask = 1 << LayerMask.NameToLayer("Fence");

			if(Physics.Raycast(transform.position, transform.forward, out hit, 4f, layerMask))
			{
				motor.motor = -1;
			}

			StuckTime += Time.deltaTime;
			if(StuckTime > 4)
			{
				StuckTime = 0;
				Flip();
			}
		}
		else
		{
			StuckTime = 0;
		}

		//Farting
		//If there's enough time, and we're facing the right direction
		if( Time.time > NextFartTime && Mathf.Abs(whichWay) < 0.1f)
		{
			//If we're going too slow or we're far away enough
			if(body.velocity.magnitude < 2 || distance > 5)
			{
				//If there's nothing in the way
				RaycastHit hit;
				int layerMask = 1 << LayerMask.NameToLayer("Fence");

				if(!Physics.Raycast(transform.position, transform.forward, out hit, 4f, layerMask))
				{
					bool fart = Random.Range(0, 10) > 7 || TryFart;
                    
					if (fart)
                    {
                        NextFartTime = Time.time + 5;
                        Fart();
						TryFart = false;
                    }
                    else
                    {
                        NextFartTime = Time.time + 0.4f;
                    }
				}
			}
		} 

		if(Time.timeSinceLevelLoad > NextHornTime)
		{			
			if(!raceManager.bRaceComplete)
			{
				GetComponent<Horn>().Play();
				GetComponent<Tracker>().Horn();
			}

			NextHornTime = Time.timeSinceLevelLoad + Random.Range(1,25);
		}
	}

	protected override void OnNextCheckPoint()
	{
		WantPosition = NextCheckPoint.transform.position;
		WantPosition.y = 0;

		Vector3 targetDir = WantPosition - transform.position;
		targetDir = targetDir.normalized;

		float dot = Vector3.Dot(targetDir, transform.forward);
		float angle = Mathf.Acos( dot ) * Mathf.Rad2Deg; 

		float whichWay = -Vector3.Cross(NextCheckPoint.transform.forward, transform.forward).y;

		float right = whichWay > 0 ? 2 : 1;
		float left = whichWay < 0 ? -2 : -1;

		WantPosition += NextCheckPoint.transform.right * Random.Range(left, right);

		if(PlaceAiMarkers)
			Instantiate(AiMarkerPrefab, WantPosition, Quaternion.identity);

		Checkpoint cp = NextCheckPoint.GetComponent<Checkpoint>();
		if(cp != null)
		{
			if(cp.MaxSpeed > 0)
				MaxSpeed = cp.MaxSpeed;
			TryFart = cp.TryFart;
			if(TryFart)
				NextFartTime = Time.time;
		}
		else
		{
			MaxSpeed = 99;
		}
	}
}

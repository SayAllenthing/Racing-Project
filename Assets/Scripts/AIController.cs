using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : CarController {

	bool TurningAround = false;

	Vector3 WantPosition;

	public bool PlaceAiMarkers;
	public GameObject AiMarkerPrefab;

	float StuckTime = 0;

	float LastFartTime = 0;

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

		motor.motor = 1;

		//Move and Steer
		if(angle < 90 && !TurningAround)
		{
			motor.steering = whichWay;

			//Sharp Turn
			if(Mathf.Abs(whichWay) > 0.5f)
			{				
				if(body.velocity.magnitude > 10f)
					motor.motor = -1f;
				else if(body.velocity.magnitude < 2)
					motor.motor = 1;
				else
					motor.motor = 0f;
			}
			else if(Mathf.Abs(whichWay) > 0.25f)//Minor Turn
			{
				if(body.velocity.magnitude > 10)
					motor.motor = 0.25f;
				else if(body.velocity.magnitude < 2)
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

		//Am I stuck Try to get Unstuck
		if(body.velocity.magnitude < 1.4f && Mathf.Abs(motor.motor) > 0.5f)
		{
			RaycastHit hit;
			int layerMask = 1 << LayerMask.NameToLayer("Fence");

			if(Physics.Raycast(transform.position, transform.forward, out hit, 2f, layerMask))
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
		if( Time.time > LastFartTime + 5 && Mathf.Abs(whichWay) < 0.1f)
		{
			//If we're going too slow or we're far away enough
			if(body.velocity.magnitude < 2 || distance > 5)
			{
				//If there's nothing in the way
				RaycastHit hit;
				int layerMask = 1 << LayerMask.NameToLayer("Fence");

				if(!Physics.Raycast(transform.position, transform.forward, out hit, 4f, layerMask))
				{
					int rand = Random.Range(0, 10);
					if(rand > 7)
					{
						LastFartTime = Time.time;
						Fart();
					}
				}
			}
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

		float whichWay = -Vector3.Cross(targetDir, transform.forward).y;

		float right = whichWay > 0 ? 2 : 1;
		float left = whichWay < 0 ? -2 : -1;

		WantPosition += NextCheckPoint.transform.right * Random.Range(left, right);

		if(PlaceAiMarkers)
			Instantiate(AiMarkerPrefab, WantPosition, Quaternion.identity);
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CarController {
	
	public PlayerNumber Number;
	public CameraController cam;

	public bool AutoInit = false;

	// Use this for initialization
	void Start () 
	{
		if(AutoInit)
			Init();
	}

	public override void Init()
	{
		base.Init();
		EnableAudio = true;
		motor.bEnableAudio = true;
	}

	public void FixedUpdate()
	{
		if(EnableAudio)
		{
			if(Input.GetButtonDown("Horn" + Number.ToString()))
			{
				GetComponent<Horn>().Play();
				if(!raceManager.bRaceComplete)
				{
					GetComponent<Tracker>().Horn();

				}
			}
		}

		if(!bIsActive)
			return;

		motor.motor = Input.GetAxis("Accel" + Number.ToString()) + Input.GetAxis("Brake" + Number.ToString());
		motor.steering = Input.GetAxis("Horizontal" + Number.ToString());

		if(transform.position.y > 2)
		{ 
			float WantRot = Input.GetAxis("Horizontal" + Number.ToString());
			transform.Rotate(Vector3.up, WantRot * 50 * Time.deltaTime);

			if(Mathf.Abs(WantRot) > 0.25f)
				body.angularVelocity = Vector3.zero;
		}


        if (Input.GetButtonDown("Boost" + Number.ToString()))
		{
			Fart();
		}

		if(Input.GetButtonDown("Flip" + Number.ToString()) && CanFlip)
		{
			Flip();	        
		}
	}

	protected override void OnRaceComplete()
	{
		base.OnRaceComplete();
		bIsActive = false;
		GetComponent<AIController>().Init();
        GetComponent<AIController>().Activate();

        cam.OnRaceComplete();
	}
}

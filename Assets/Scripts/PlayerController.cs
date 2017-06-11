using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CarController {
	
	public PlayerNumber Number;
	public CameraController cam;

	public override void Init()
	{
		base.Init();
		motor.bEnableAudio = true;
	}

	public void FixedUpdate()
	{		
		if(!bIsActive)
			return;

		motor.motor = Input.GetAxis("Accel" + Number.ToString()) + Input.GetAxis("Brake" + Number.ToString());
		motor.steering = Input.GetAxis("Horizontal" + Number.ToString());

		if(Input.GetButtonDown("Boost" + Number.ToString()))
		{
			Fart();
		}

		if(Input.GetButtonDown("Flip" + Number.ToString()) && CanFlip)
			Flip();	
	}

	protected override void OnRaceComplete()
	{
		base.OnRaceComplete();
		bIsActive = false;
		GetComponent<AIController>().Init();

		cam.OnRaceComplete();
	}
}

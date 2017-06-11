using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VehicleMotor : MonoBehaviour 
{
	public List<AxleInfo> axleInfos; // the information about each individual axle
	public float maxMotorTorque; // maximum torque the motor can apply to wheel
	public float maxSteeringAngle; // maximum steer angle the wheel can have

	public float motor;
	public float steering;

	[HideInInspector]
	public bool bEnableAudio = false;

	public AudioSource EngineNoise;

	// finds the corresponding visual wheel
	// correctly applies the transform
	public void ApplyLocalPositionToVisuals(WheelCollider collider, Vector3 offset)
	{
		if (collider.transform.childCount == 0) {
			return;
		}

		Transform visualWheel = collider.transform.GetChild(0);

		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);

		visualWheel.transform.localPosition = offset;
		visualWheel.transform.rotation = rotation;
	}

	public void FixedUpdate()
	{
		//motor = maxMotorTorque * Input.GetAxis("Vertical");
		//float steering = maxSteeringAngle * Input.GetAxis("Horizontal");

		foreach (AxleInfo axleInfo in axleInfos) 
		{
			if (axleInfo.steering) 
			{
				axleInfo.leftWheel.steerAngle = steering * maxSteeringAngle;
				axleInfo.rightWheel.steerAngle = steering * maxSteeringAngle;
			}
			if (axleInfo.motor) 
			{
				axleInfo.leftWheel.motorTorque = motor * maxMotorTorque;
				axleInfo.rightWheel.motorTorque = motor * maxMotorTorque;

			}

			ApplyLocalPositionToVisuals(axleInfo.leftWheel, axleInfo.Offset);
			ApplyLocalPositionToVisuals(axleInfo.rightWheel, axleInfo.Offset);
		}

		if(EngineNoise != null && bEnableAudio)
		{
			float volume = motor;
			EngineNoise.volume = Mathf.Clamp(volume, 0.5f, volume);
		}
	}

	public void Boost()
	{
		GetComponent<Rigidbody>().AddForce(transform.forward * 500000);
	}
}

[System.Serializable]
public class AxleInfo 
{
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor; // is this wheel attached to motor?
	public bool steering; // does this wheel apply steer angle?
	public Vector3 Offset;
}

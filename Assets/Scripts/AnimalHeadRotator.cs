using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalHeadRotator : MonoBehaviour {

	float rotationTime = 0;
	float WantRotation = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		float rot = transform.localEulerAngles.z;
		rot = Mathf.Lerp(rot, WantRotation, Time.deltaTime * 5);

		Vector3 rotation = transform.localEulerAngles;
		rotation.z = rot;
		transform.localEulerAngles = rotation;

		if(Time.time > rotationTime)
		{
			PickRotation();
		}
	}

	void PickRotation()
	{
		WantRotation = Random.Range(-20, 20);
		rotationTime = Time.time + 5;
	}

}

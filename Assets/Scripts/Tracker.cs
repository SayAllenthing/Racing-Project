using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tracker : MonoBehaviour {

	CarController Car;
	Rigidbody Body;

	public float HighestAltitude = 0;
	public float FastestSpeed = 0;
	public int NumFarts = 0;
	public int CornHit = 0;
	public int HornPressed = 0;

	public void Init(CarController car)
	{
		Car = car;
		Body = car.GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () 
	{
		if(Car == null)
			return;

		if(Car.transform.position.y > HighestAltitude)
		{
			HighestAltitude = Car.transform.position.y;
		}

		if(Body.velocity.magnitude > FastestSpeed)
		{
			FastestSpeed = Body.velocity.magnitude;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Corn")
		{
			CornHit++;
		}
	}

	public void Fart()
	{
		NumFarts++;
	}

	public void Horn()
	{
		HornPressed++;
	}
}

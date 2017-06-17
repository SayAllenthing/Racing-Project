using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siren : MonoBehaviour 
{
	public Light LightOne;
	public Light LightTwo;

	public MeshRenderer RedLight;
	public Material RedOff;
	public Material RedOn;

	public MeshRenderer BlueLight;
	public Material BlueOff;
	public Material BlueOn;

	bool bEnabled = false;

	// Use this for initialization
	void Start () 
	{
		GetComponent<Horn>().OnHorn += Toggle;
	}
	
	// Update is called once per frame
	void Update () 
	{
		LightOne.transform.Rotate(0,15,0, Space.World);
		LightTwo.transform.Rotate(0,15,0, Space.World);

		if(bEnabled)
		{
			RedLight.material = Random.Range(0,3) > 1 ? RedOn : RedOff;
			BlueLight.material = Random.Range(0,3) > 1 ? BlueOn : BlueOff;				 
		}
	}

	void Toggle()
	{
		bEnabled = !bEnabled;

		LightOne.enabled = bEnabled;
		LightTwo.enabled = bEnabled;


		RedLight.material = bEnabled ? RedOn : RedOff;
		BlueLight.material = bEnabled ? BlueOn : BlueOff;		
	}
}

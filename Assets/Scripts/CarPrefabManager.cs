using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPrefabManager : MonoBehaviour {

	public static CarPrefabManager Instance;

	public List<GameObject> Cars;

	// Use this for initialization
	void Start () 
	{
		if(Instance != null)
		{
			Destroy(this.gameObject);
			return;
		}

		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

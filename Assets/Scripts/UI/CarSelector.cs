using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSelector : MonoBehaviour {

	public CarPrefabManager prefabs;

	public List<GameObject> Cars;
	public int CurrentCar = 0;
	public int CurrentHat = 0;

    int MaxCars = 1;
    int MaxHats = 1;

	int Width = 15;

	Vector3 Offset = new Vector3(0, -2, -3f);
	public Vector3 WantPosition;

	// Use this for initialization
	void Start () 
	{
		WantPosition = Offset;
		for(int i = 0; i < prefabs.Cars.Count; i++)
		{
			GameObject g = GameObject.Instantiate(prefabs.Cars[i], new Vector3(i * Width, 0, 0), Quaternion.identity, transform);
			g.GetComponent<Rigidbody>().isKinematic = true;
			g.AddComponent(typeof(Rotator));
			g.SetActive(false);
			SetLayer(g, gameObject.layer);
			Cars.Add(g);
		}

        MaxCars = Cars.Count - 1;
		InitHats();
	}

	void SetLayer(GameObject g, int l)
	{
		g.layer = l;

		foreach(Transform t in g.transform)
		{
			SetLayer( t.gameObject, l);
		}
	}

	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, WantPosition, 20 * Time.deltaTime);
	}

	public void Show()
	{
		Cars[CurrentCar].SetActive(true);
	}

	public void Hide()
	{
		Cars[CurrentCar].SetActive(false);
	}

	public void ChangeCar(bool left)
	{
		int prev = CurrentCar;

		if(left)
		{
			if(CurrentCar == 0)
			{
				transform.position = new Vector3(-MaxCars * Width, 0, 0) + Offset;
				CurrentCar = MaxCars - 1;
			}
			else
			{
				CurrentCar--;
			}
		}
		else
		{
			if(CurrentCar == MaxCars-1)
			{
				transform.position = new Vector3(0 + Width, 0, 0) + Offset;
				CurrentCar = 0;
			}
			else
			{
				CurrentCar++;
			}
		}

		Cars[prev].SetActive(false);
		Cars[CurrentCar].SetActive(true);

		WantPosition = new Vector3(-CurrentCar * Width, 0, 0) + Offset;
	}

	public void InitHats()
	{
		foreach(GameObject g in Cars)
		{			
			g.GetComponent<PlayerController>().pig.InitHat();
		}
	}

	public void ChangeHat(bool left)
	{
		foreach(GameObject g in Cars)
		{
			CurrentHat = g.GetComponent<PlayerController>().pig.ChangeHat(left);
		}
	}

    public void OnCheat()
    {
        MaxCars = Cars.Count;
        foreach (GameObject g in Cars)
        {			
            g.GetComponent<PlayerController>().pig.OnHatCheat();
        }
    }
}

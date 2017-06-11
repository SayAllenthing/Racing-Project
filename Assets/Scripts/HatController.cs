using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour {

	public List<GameObject> Hats = new List<GameObject>();

	int CurrentHat = 0;

	void Start()
	{
		SetHat();
	}

	void SetHat()
	{
		for(int i = 0; i < Hats.Count; i++)
		{
			if(i == CurrentHat)
				Hats[i].SetActive(true);
			else
				Hats[i].SetActive(false);
		}
	}

	public void SetCurrentHat(int index)
	{
		CurrentHat = index;
		SetHat();
	}

	public int ChangeHat(bool left)
	{
		if(left)
		{
			if(CurrentHat - 1 < 0)
			{
				CurrentHat = Hats.Count - 1;
			}
			else
			{
				CurrentHat--;
			}
		}
		else
		{
			if(CurrentHat + 1 == Hats.Count)
			{
				CurrentHat = 0;
			}
			else
			{
				CurrentHat++;
			}
		}

		SetHat();
		return CurrentHat;
	}
}

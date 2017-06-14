using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour {

	public List<GameObject> Hats = new List<GameObject>();

	int CurrentHat = 0;
    int MaxHats = 1;

	void Start()
	{
		SetHat();
        MaxHats = Hats.Count - 1;
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

    public void OnCheat()
    {
        MaxHats = Hats.Count;
    }

	public int ChangeHat(bool left)
	{
		if(left)
		{
			if(CurrentHat - 1 < 0)
			{
				CurrentHat = MaxHats - 1;
			}
			else
			{
				CurrentHat--;
			}
		}
		else
		{
			if(CurrentHat + 1 == MaxHats)
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

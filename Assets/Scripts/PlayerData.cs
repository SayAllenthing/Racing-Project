using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerNumber
{
	One,
	Two,
	Three,
	Four
}

public class PlayerData : MonoBehaviour 
{
	public bool bIsActive = false;

	public int CarPrefab = 0;
	public int HatPrefab = 0;

	public PlayerNumber Number;
}

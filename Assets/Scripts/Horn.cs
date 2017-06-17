using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horn : MonoBehaviour {

	public enum HornType
	{
		Burst,
		Toggle
	}

	public HornType Type;

	public AudioSource HornSource;

	public delegate void HornDelegate();
	public HornDelegate OnHorn;

	public void Play()
	{
		if(Type == HornType.Burst)
		{			
			HornSource.Play();
		}
		else
		{
			HornSource.loop = true;
			if(HornSource.isPlaying)
				HornSource.Stop();
			else
				HornSource.Play();
		}

		if(OnHorn != null)
			OnHorn();
	}
}

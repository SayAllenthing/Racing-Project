using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : MonoBehaviour 
{
	public ParticleSystem FartEffect;

	public List<AudioClip> FartNoises = new List<AudioClip>();
	public AudioSource FartSource;

	public HatController Hats;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetHat(int hat)
	{
		Hats.SetCurrentHat(hat);
	}

	public int ChangeHat(bool left)
	{
		return Hats.ChangeHat(left);
	}

	public void OnFart()
	{
		FartEffect.Play();

		FartSource.clip = FartNoises[Random.Range(0, FartNoises.Count)];
		FartSource.Play();
	}
}

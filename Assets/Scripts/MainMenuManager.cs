using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour {

		
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Submit"))
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("SelectScreen");
		}
	}
}

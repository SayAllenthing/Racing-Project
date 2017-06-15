using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Award : MonoBehaviour {

	public TextMeshProUGUI Title;
	public TextMeshProUGUI Desc;

	public void Init(string title, string desc)
	{
		Title.text = title;
		Desc.text = desc;
	}
}

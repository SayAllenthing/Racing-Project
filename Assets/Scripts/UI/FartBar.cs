using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FartBar : MonoBehaviour {

    public Color Low;
    public Color High;

    public Image bar;

    float maxHeight = 193;

	// Use this for initialization
	void Start () {
		
	}
	
	public void SetFillAmount(float amount)
    {
        bar.rectTransform.sizeDelta = new Vector2(52, maxHeight * amount);
        bar.color = Color.Lerp(Low, High, amount);
    }
}

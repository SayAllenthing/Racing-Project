using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VelocityText : MonoBehaviour {

    public Rigidbody body;
    public Text text;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        text.text = "Speed: " + (body.velocity.magnitude * 2f).ToString("0");
	}
}

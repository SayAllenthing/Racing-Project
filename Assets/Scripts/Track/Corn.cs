using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corn : MonoBehaviour {

    float KillTimer = -1;

	// Use this for initialization
	void Start () {
		
	}

    private void Update()
    {
        if(KillTimer > 0)
        {
            KillTimer -= Time.deltaTime;
            if(KillTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag != "Terrain")
        {
            gameObject.layer = LayerMask.NameToLayer("Ignore Default");
            KillTimer = 2;
        }
    }
}

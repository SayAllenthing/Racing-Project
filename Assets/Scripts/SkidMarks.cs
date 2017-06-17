using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidMarks : MonoBehaviour 
{

	public Transform Container;

    public WheelCollider CorrespondingCollider;
    public GameObject SkidMarkPrefab;

	public GameObject CurrentSkid;


    void Update()
    {
        // define a wheelhit object, this stores all of the data from the wheel collider and will allow us to determine
        // the slip of the tire.
        WheelHit CorrespondingGroundHit;
        CorrespondingCollider.GetGroundHit(out CorrespondingGroundHit);

        // if the slip of the tire is greater than 2.0, and the slip prefab exists, create an instance of it on the ground at
        // a zero rotation.

        if (Mathf.Abs(CorrespondingGroundHit.sidewaysSlip) > 0.175f)
        {
			if(CurrentSkid == null)
			{
				CreateSkid();
			}
        }
        else if (Mathf.Abs(CorrespondingGroundHit.sidewaysSlip) <= 0.075f)
        {
			if(CurrentSkid != null)
			{
				ReleaseSkid();
			}
        }
    }

	void CreateSkid()
	{
		CurrentSkid = GameObject.Instantiate(SkidMarkPrefab, Container);
	}

	void ReleaseSkid()
	{
		CurrentSkid.transform.SetParent(null);
		CurrentSkid = null;
	}
}

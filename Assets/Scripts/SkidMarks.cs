using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkidMarks : MonoBehaviour {

    public WheelCollider CorrespondingCollider;
    public GameObject skidMarkPrefab;

    void Start()
    {
        skidMarkPrefab.gameObject.SetActive(false);
    }

    void Update()
    {

        // define a hit point for the raycast collision
        RaycastHit hit;
        // Find the collider's center point, you need to do this because the center of the collider might not actually be
        // the real position if the transform's off.
        Vector3 ColliderCenterPoint = CorrespondingCollider.transform.TransformPoint(CorrespondingCollider.center);

        /*
        // now cast a ray out from the wheel collider's center the distance of the suspension, if it hit something, then use the "hit"
        // variable's data to find where the wheel hit, if it didn't, then se tthe wheel to be fully extended along the suspension.
        if (Physics.Raycast(ColliderCenterPoint, -CorrespondingCollider.transform.up, out hit, CorrespondingCollider.suspensionDistance + CorrespondingCollider.radius))
        {
            transform.position = hit.point + (CorrespondingCollider.transform.up * CorrespondingCollider.radius) + new Vector3(-1.15f, 0, -0.4f);
        }
        else
        {
            transform.position = ColliderCenterPoint - (CorrespondingCollider.transform.up * CorrespondingCollider.suspensionDistance) + new Vector3(-1.15f, 0, -0.4f);
        }
        */

        // define a wheelhit object, this stores all of the data from the wheel collider and will allow us to determine
        // the slip of the tire.
        WheelHit CorrespondingGroundHit;
        CorrespondingCollider.GetGroundHit(out CorrespondingGroundHit);

        // if the slip of the tire is greater than 2.0, and the slip prefab exists, create an instance of it on the ground at
        // a zero rotation.

        if (Mathf.Abs(CorrespondingGroundHit.sidewaysSlip) > 0.15f)
        {
            skidMarkPrefab.gameObject.SetActive(true);
        }
        else if (Mathf.Abs(CorrespondingGroundHit.sidewaysSlip) <= 0.01f)
        {
            skidMarkPrefab.gameObject.SetActive(false);
        }
    }
}

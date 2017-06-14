using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Camera cam;
	private Vector3 velocity = Vector3.zero;
	public Transform target;
	public PlayerUI playerUI;

	public RectTransform Panel;

	bool RaceComplete = false;

	public void SetTarget(CarController t)
	{		
		target = t.transform;
		cam.enabled = true;

		playerUI.Init(t);
	}

	// Update is called once per frame
	void Update () 
	{
		if (target)
		{			
			transform.position = target.position; //Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);

			if(RaceComplete)
			{
				transform.Rotate(Vector3.up, -50 * Time.deltaTime);
			}
			else
			{
				transform.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
			}
		}
	}

	public void OnRaceComplete()
	{
		RaceComplete = true;
	}
}
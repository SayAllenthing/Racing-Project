using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISelectController : MonoBehaviour {

	public PlayerData Player;
	public CanvasGroup Overlay;
	public CanvasGroup ReadyPanel;

	int CurrentSelection = 0;
	public CanvasGroup VehicleSelect;
	public CanvasGroup HatSelect;

	public CarSelector Selector;

	public SelectScreenMananger screenManager;

	public CanvasGroup SelectPanel;

	bool bEnabled = false;
	bool bReady = false;
	bool HorizontalLock = false;
	bool VerticalLock = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Confirm" + Player.Number.ToString()))
		{            
			if(!bEnabled)
			{
				Enable();
			}
			else if(!bReady)
			{
				Ready();
			}
		}

		if(Input.GetButtonDown("Boost" + Player.Number.ToString()))
		{
			if(bEnabled && !bReady)
			{
				Disable();
			}

			if(bReady)
			{
				UnReady();
			}
		}

		if(!bEnabled || bReady)
			return;

		float x = Input.GetAxis("Horizontal" + Player.Number.ToString());
		if(Mathf.Abs(x) > 0.7f && !HorizontalLock)
		{
			if(CurrentSelection == 0)
				Selector.ChangeCar(x < 0);
			else
				Selector.ChangeHat(x < 0);

			HorizontalLock = true;
		}
		else if(Mathf.Abs(x) < 0.3f && HorizontalLock)
		{
			HorizontalLock = false;
		}

		float y = Input.GetAxis("Vertical" + Player.Number.ToString());
		if(Mathf.Abs(y) > 0.7f && !VerticalLock)
		{
			CurrentSelection = CurrentSelection == 0 ? 1 : 0;
			ShowSelection();
			VerticalLock = true;
		}
		else if(Mathf.Abs(y) < 0.3f && VerticalLock)
		{
			VerticalLock = false;
		}

	}

	void Enable()
	{
		bEnabled = true;
		Overlay.alpha = 0;
		Selector.Show();

		screenManager.ActivatePlayer(Player);

		ShowSelection();
	}

	void Disable()
	{
		bEnabled = false;
		Overlay.alpha = 1;
		Selector.Hide();

		screenManager.UnactivatePlayer(Player);

		HideSelection();
	}

	void Ready()
	{
		bReady = true;

		ReadyPanel.alpha = 1;

		Player.CarPrefab = Selector.CurrentCar;
		Player.HatPrefab = Selector.CurrentHat;
		screenManager.ReadyPlayer(Player);

		HideSelection();
		SelectPanel.alpha = 0;
	}

	void UnReady()
	{
		bReady = false;

		ReadyPanel.alpha = 0;

		screenManager.UnreadyPlayer(Player);

		ShowSelection();
		SelectPanel.alpha = 1;
	}

	void ShowSelection()
	{
		if(CurrentSelection == 0)
		{
			VehicleSelect.alpha = 1;
			HatSelect.alpha = 0;
		}
		else
		{
			VehicleSelect.alpha = 0;
			HatSelect.alpha = 1;
		}
	}

	void HideSelection()
	{
		VehicleSelect.alpha = 0;
		HatSelect.alpha = 0;
	}
}

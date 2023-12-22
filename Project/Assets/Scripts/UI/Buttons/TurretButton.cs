using System;
using UnityEngine;
using UnityEngine.UI;



public class TurretButton : SelectableButton //this button is used to select turrets for creating a new turret group
{
	public event EventHandler<EventArgs> OnClick;

	public Turret Turret {get{return _turret;}}

	private Turret _turret;



	protected override void Start ()
	{
		base.Start ();

		onClick.AddListener (CallOnClick);
	}



	public void Initiate (Turret turret)
	{
		_turret = turret;

		GetComponentInChildren<Text> ().text = turret.gameObject.name;
	}



	private void CallOnClick ()
	{
		if (OnClick != null)
			OnClick (this, new EventArgs ());
	}
}

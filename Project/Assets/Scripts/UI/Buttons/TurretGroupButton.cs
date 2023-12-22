using System;
using UnityEngine;
using UnityEngine.UI;



public class TurretGroupButton : SelectableButton
{
	public event EventHandler<EventArgs> OnClick;

	public TurretGroup Group {get{return _group;}}

	private TurretGroup _group;



	protected override void Start ()
	{
		base.Start ();

		onClick.AddListener (CallOnClick);
	}

	public void Initiate (TurretGroup group)
	{
		_group = group;

		GetComponentInChildren<Text> ().text = group.Name;
	}

	private void CallOnClick ()
	{
		if (OnClick != null)
			OnClick (this, new EventArgs ());
	}
}

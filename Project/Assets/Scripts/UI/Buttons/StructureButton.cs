using System;
using UnityEngine;
using UnityEngine.UI;

public class StructureButton : Button
{
	public event EventHandler<EventArgs> OnClick;

	public Structure Structure {get{return _structure;}}

	private Structure _structure;



	void Start ()
	{
		onClick.AddListener (CallOnClick);
	}

	public void Initiate (Structure structure)
	{
		_structure = structure;

		GetComponentInChildren<Text> ().text = structure.Data.Name;
	}

	private void CallOnClick ()
	{
		if (OnClick != null)
			OnClick (this, new EventArgs ());
	}
}

using System;
using UnityEngine;
using UnityEngine.UI;



public class PartButton : Button
{
	public event EventHandler<EventArgs> OnClick;

	public Part StructurePart {get{return _structurePart;}}

	[SerializeField]
	private Part _structurePart;



	void Start ()
	{
		onClick.AddListener (CallOnClick);
	}

	public void Initiate (Part part)
	{
		_structurePart = part;

		GetComponentInChildren<Text> ().text = part.gameObject.name;
	}

	private void CallOnClick ()
	{
		if (OnClick != null)
			OnClick (this, new EventArgs ());
	}
}

using UnityEngine;
using UnityEngine.UI;



public class SelectableButton : Button
{
	public bool IsSelected {get{return _isSelected;}}

	[SerializeField]
	protected ColorBlock _selectedColors;

	protected bool _isSelected;//TODO ask more expirienced programmer, whether this is acceptable or not
	protected ColorBlock _normalColors;



	protected virtual void Start ()
	{
		_normalColors = colors;

		onClick.AddListener (OnClicked);
	}

	public void SetSelected (bool state)
	{
		_isSelected = state;

		if (_isSelected)
			colors = _selectedColors;
		else
			colors = _normalColors;
	}

	public void OnClicked ()
	{
		_isSelected = !_isSelected;

		if (_isSelected)
			colors = _selectedColors;
		else
			colors = _normalColors;
	}
}

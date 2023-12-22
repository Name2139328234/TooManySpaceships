using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class TurretGroupsUI : MonoBehaviour
{
	[SerializeField]
	private Transform _listLocation;
	[SerializeField]
	private Vector3 _listGrowthDistances;
	[SerializeField]
	private TurretGroupButton _turretGroupButtonTemplate;
	[SerializeField]
	private ColorBlock _pressedButtonColors;
	[SerializeField]
	private Vector3 _selectTargetButtonRelativePosition;
	[SerializeField]
	private Button _selectTargetButton;
	[SerializeField]
	private Vector3 _fireAtWillButtonRelativePosition;
	[SerializeField]
	private Button _fireAtWillButton;
	[SerializeField]
	private Vector3 _lockOnButtonRelativePosition;
	[SerializeField]
	private Button _lockOnButton;
	[SerializeField]
	private Vector3 _closeButtonRelativePosition;
	[SerializeField]
	private Button _closeButton;

	private PlayerHuman _player;
	private Structure _selectedStructure;
	private TurretGroupButton _selectedTurretGroupButton;
	private ColorBlock _fireAtWillButtonDefaultColors;
	private ColorBlock _lockOnButtonDefaultColors;
	private List<TurretGroupButton> _turretGroupButtons;



	void Start ()
	{
		_turretGroupButtons = new List<TurretGroupButton> ();

		_selectTargetButton.onClick.AddListener (SelectTarget);
		_fireAtWillButton.onClick.AddListener (InvertFireAtWill);
		_lockOnButton.onClick.AddListener (InvertTracking);
		_closeButton.onClick.AddListener (HideTurretGroupOptions);

		_fireAtWillButtonDefaultColors = _fireAtWillButton.colors;
		_lockOnButtonDefaultColors = _lockOnButton.colors;
	}

	public void SelectStructure (Structure structure)
	{
		if (_selectedStructure != null)
			_selectedStructure.OnAddedTurretGroup -= OnAddedTurretGroup;

		_selectedStructure = structure;

		_selectedStructure.OnAddedTurretGroup += OnAddedTurretGroup;

		ShowAllGroups ();
	}

	public void SelectPlayer (PlayerHuman player)
	{
		_player = player;
	}

	private void OnAddedTurretGroup (object sender, TurretGroupArgs e)
	{
		TurretGroupButton button = Instantiate (_turretGroupButtonTemplate.gameObject, _listLocation).GetComponent<TurretGroupButton> ();

		button.Initiate (e.Group);

		button.transform.position = _listLocation.position + _listGrowthDistances * _turretGroupButtons.Count;

		button.OnClick += TurretGroupButtonOnClick;

		_turretGroupButtons.Add (button);
	}

	private void ShowAllGroups ()
	{
		for (int i = 0; i < _selectedStructure.TurretGroups.Length; i++)
		{
			TurretGroupButton button = Instantiate (_turretGroupButtonTemplate.gameObject, _listLocation).GetComponent<TurretGroupButton> ();

			button.Initiate (_selectedStructure.TurretGroups[i]);

			button.transform.position = _listLocation.position + _listGrowthDistances * i;

			button.OnClick += TurretGroupButtonOnClick;

			_turretGroupButtons.Add (button);
		}
	}

	private void TurretGroupButtonOnClick (object sender, System.EventArgs e)
	{
		if (_selectedTurretGroupButton != sender as TurretGroupButton && _selectedTurretGroupButton != null && _selectedTurretGroupButton.IsSelected)//TODO ask for quality check on this part, it does not feel good
		{
			_selectedTurretGroupButton.SetSelected (false);
		}
		
		ShowTurretGroupOptions (sender as TurretGroupButton);

		_player.SelectTurretGroup ((sender as TurretGroupButton).Group);

		_selectedTurretGroupButton = sender as TurretGroupButton;
	}

	private void ShowTurretGroupOptions (TurretGroupButton button)
	{
		_selectTargetButton.transform.localPosition = button.transform.localPosition + _selectTargetButtonRelativePosition;
		_fireAtWillButton.transform.localPosition = button.transform.localPosition + _fireAtWillButtonRelativePosition;
		_lockOnButton.transform.localPosition = button.transform.localPosition + _lockOnButtonRelativePosition;
		_closeButton.transform.localPosition = button.transform.localPosition + _closeButtonRelativePosition;

		if (button.Group.FireAtWill)
			_fireAtWillButton.colors = _pressedButtonColors;
		else
			_fireAtWillButton.colors = _fireAtWillButtonDefaultColors;

		if (button.Group.Track)
			_lockOnButton.colors = _pressedButtonColors;
		else
			_lockOnButton.colors = _lockOnButtonDefaultColors;

		_selectTargetButton.gameObject.SetActive (true);
		_fireAtWillButton.gameObject.SetActive (true);
		_lockOnButton.gameObject.SetActive (true);
		_closeButton.gameObject.SetActive (true);
	}

	private void HideTurretGroupOptions ()
	{
		_selectTargetButton.gameObject.SetActive (false);
		_fireAtWillButton.gameObject.SetActive (false);
		_lockOnButton.gameObject.SetActive (false);
		_closeButton.gameObject.SetActive (false);
	}

	private void InvertTracking ()
	{
		//the fact that the button calling this is active guarantees that selectedTurretGroupButton exists
		_selectedTurretGroupButton.Group.SetTracking (!_selectedTurretGroupButton.Group.Track);

		if (_selectedTurretGroupButton.Group.Track)
			_lockOnButton.colors = _pressedButtonColors;
		else
			_lockOnButton.colors = _lockOnButtonDefaultColors;
	}

	private void InvertFireAtWill ()
	{
		//the fact that the button calling this is active guarantees that selectedTurretGroupButton exists
		_selectedTurretGroupButton.Group.SetFireAtWill (!_selectedTurretGroupButton.Group.FireAtWill);

		if (_selectedTurretGroupButton.Group.FireAtWill)
			_fireAtWillButton.colors = _pressedButtonColors;
		else
			_fireAtWillButton.colors = _fireAtWillButtonDefaultColors;
	}

	private void SelectTarget ()
	{
		if (_player.SelectedTarget != null)
			_selectedTurretGroupButton.Group.Aim (_player.SelectedTarget.transform);
	}
}

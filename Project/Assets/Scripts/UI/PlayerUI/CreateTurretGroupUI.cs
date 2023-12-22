using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class CreateTurretGroupUI : MonoBehaviour
{
	[SerializeField]
	private TurretButton _turretButtonTemplate;
	[SerializeField]
	private Transform _listLocation;
	[SerializeField]
	private Vector3 _turretListGrowthDistances;
	[SerializeField]
	private Button _startCreationButton;
	[SerializeField]
	private Button _finishCreationButton;
	[SerializeField]
	private InputField _groupName;

	private List<TurretButton> _turretButtons;
	private List<Turret> _turretsForGroup;
	private PlayerHuman _player;



	void Start ()
	{
		_turretButtons = new List<TurretButton> ();
		_turretsForGroup = new List<Turret> ();

		_startCreationButton.onClick.AddListener (StartGroupCreation);

		_finishCreationButton.onClick.AddListener (FinishGroupCreation);
	}

	public void SelectPlayer (PlayerHuman player)
	{
		_player = player;
	}

	private void CreateButtonList ()
	{
		int turretCounter = 0;//is necessary, because "i" reflects the amount of every ship parts, not the amount of turrets

		for (int i = 0; i < _player.SelectedStructure.Data.Parts.Length; i++)
		{
			if (_player.SelectedStructure.Data.Parts[i] is Turret)
			{
				TurretButton button = Instantiate (_turretButtonTemplate.gameObject, _listLocation).GetComponent<TurretButton> ();

				button.Initiate (_player.SelectedStructure.Data.Parts[i] as Turret);

				button.transform.position = _listLocation.position + _turretListGrowthDistances * turretCounter;

				button.OnClick += AddTurretToList;

				_turretButtons.Add (button);

				turretCounter++;
			}
		}
	}

	private void ClearButtonList ()
	{
		foreach (TurretButton button in _turretButtons)
		{
			Destroy (button.gameObject);
		}

		_turretButtons.Clear ();
	}

	private void AddTurretToList (object sender, System.EventArgs e)
	{
		_turretsForGroup.Add ((sender as TurretButton).Turret);
	}

	private void StartGroupCreation ()
	{
		ClearButtonList ();

		CreateButtonList ();

		_groupName.gameObject.SetActive (true);
		_finishCreationButton.gameObject.SetActive (true);
	}

	private void FinishGroupCreation ()
	{

		_groupName.gameObject.SetActive (false);
		_finishCreationButton.gameObject.SetActive (false);

		TurretGroup newGroup = _player.SelectedStructure.gameObject.AddComponent<TurretGroup> ();

		newGroup.Initiate (_groupName.text, _turretsForGroup.ToArray ());

		_player.SelectedStructure.AddTurretGroup (newGroup);


		_turretsForGroup.Clear ();

		ClearButtonList ();
	}
}
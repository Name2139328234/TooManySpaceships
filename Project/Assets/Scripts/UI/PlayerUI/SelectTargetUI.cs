using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class SelectTargetUI : MonoBehaviour
{
	[SerializeField]
	private Transform _listLocation;
	[SerializeField]
	private Vector3 _listGrowthDistances;
	[SerializeField]
	private StructureButton _structureButtontemplate;
	[SerializeField]
	private ColorBlock _pressedButtonColors;

	private PlayerHuman _player;
	private List<StructureButton> _structureButtons;



	void Start ()
	{
		_structureButtons = new List<StructureButton> ();

		CreateButtons ();
	}

	void Update ()
	{
		UpdateButtonsPositions ();
	}



	public void SelectPlayer (PlayerHuman player)
	{
		_player = player;
	}



	private void CreateButtons ()
	{
		for (int i = 0; i < LevelInfo.Instance.LoadedStructures.Length; i++)
		{
			StructureButton button = Instantiate (_structureButtontemplate.gameObject, _listLocation).GetComponent<StructureButton> ();

			button.Initiate (LevelInfo.Instance.LoadedStructures[i]);

			button.transform.position = _listLocation.position + _listGrowthDistances * i;

			button.OnClick += StructureButtonOnClick;

			_structureButtons.Add (button);

			button.Structure.OnDeath += DestroyButton;
		}
	}

	private void DestroyButton (object sender, System.EventArgs e)
	{
		foreach (StructureButton button in _structureButtons)
		{
			if (button.Structure == sender as Structure)
			{
				//_structureButtons.Remove (button);//TODO cant do that for some ungodly reason. Will this work? Learning more about collections interacting with NULLs required
				Destroy (button.gameObject);

				//TODO recalculate positions of all buttons after that
			}
		}
	}

	private void StructureButtonOnClick (object sender, System.EventArgs e)
	{
		_player.SelectTarget ((sender as StructureButton).Structure);
	}

	private void UpdateButtonsPositions ()
	{
		foreach (StructureButton button in _structureButtons)
		{
			if (button != null)
				button.transform.position = Camera.main.WorldToScreenPoint (button.Structure.transform.position);
		}
	}
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ShipConstructionUI : MonoBehaviour
{
	public Button FinishCreationButton {get{return _finishCreationButton;}}
	public InputField ShipName {get{return _shipName;}}

	[SerializeField]
	private SpaceShipConstructor _constructor;
	[SerializeField]
	private Button _StartNewShipButton;
	[SerializeField]
	private Button _editButton;
	[SerializeField]
	private Button _finishEditingButton;
	[SerializeField]
	private Transform _partButtonsListLocation;
	[SerializeField]
	private PartButton _partButtonTemplate;
	[SerializeField]
	private Vector3 _partListGrowthDistances;
	[SerializeField]
	private Button _finishCreationButton;
	[SerializeField]
	private InputField _shipName;
	[SerializeField]
	private InputField _shipLength;
	[SerializeField]
	private InputField _shipHeight;
	[SerializeField]
	private InputField _shipWidth;

	private List<PartButton> _partSelectionButtons;



	void Start ()
	{
		_partSelectionButtons = new List<PartButton> ();

		_StartNewShipButton.onClick.AddListener (StartShip);
		_finishCreationButton.onClick.AddListener (FinishShip);
		_editButton.onClick.AddListener (StartEditing);
		_finishEditingButton.onClick.AddListener (FinishEditing);

		CreatePartButtons ();
	}



	private void CreatePartButtons ()
	{
		for (int i = 0; i < _constructor.ShipPartsAvailable.Length; i++)
		{
			PartButton button = Instantiate (_partButtonTemplate.gameObject, _partButtonsListLocation).GetComponent<PartButton> ();

			button.Initiate (_constructor.ShipPartsAvailable[i]);

			button.transform.position = _partButtonsListLocation.position + _partListGrowthDistances * i;

			button.OnClick += SelectPart;
		}
	}

	void SelectPart (object sender, System.EventArgs e)
	{
		_constructor.StartAddingPart ((sender as PartButton).StructurePart);
	}

	private void StartShip ()
	{
		int length = 1;
		int height = 1;
		int width = 1;
		
		int.TryParse (_shipLength.text, out length);
		int.TryParse (_shipWidth.text, out height);
		int.TryParse (_shipHeight.text, out width);

		_constructor.StartShip (new Vector3Int (length, height, width), _shipName.text);
	}

	private void FinishShip ()
	{
		_constructor.FinishCreation ();
	}

	private void StartEditing ()
	{
		_constructor.EditShip ();
	}

	private void FinishEditing ()
	{
		_constructor.FinishEditing ();
	}
}
using System;
using System.Collections.Generic;
using UnityEngine;



public class SpaceShipConstructor : MonoBehaviour//TODO bad design. Unavoidable without complete code restructure. Creates a need to add new class for every type of structure
{
	public Part[] ShipPartsAvailable {get{return _shipPartsAvailable.ToArray ();}}

	[SerializeField]
	private Transform _partsTemporaryParent;
	[SerializeField]
	private PlayerHuman _player;
	[SerializeField]
	private ConstructionSettings _settings;
	[SerializeField]
	private List<Part> _shipPartsAvailable;

	//private Part[,,] _ship;
	private Vector3Int _dimensions;
	private Part _selectedPart;
	private SpaceShip _shipInConstruction;//TODO edit existing spaceships
	private Vector3Int _selectedPosition;



	void Start ()
	{
		_selectedPosition = new Vector3Int ();
	}

	void Update ()
	{
		MoveSelectedPosition ();

		CheckFinishKey ();
		CheckDeleteKey ();
	}



	public void StartShip (Vector3Int dimensions, string name)
	{
		_dimensions = dimensions;

		//_ship = new Part[length, height, width];

		_selectedPosition = new Vector3Int (0, 0, 0);


		GameObject shipObject = new GameObject (name);

		ShipData data = shipObject.AddComponent<ShipData> ();
		data.Initiate (_player.Team, name, dimensions);

		Rigidbody physics = shipObject.AddComponent<Rigidbody> ();
		physics.drag = GameRules.Instance.PhysicsSettings.ShipDrag;
		physics.angularDrag = GameRules.Instance.PhysicsSettings.ShipAngularDrag;
		physics.useGravity = GameRules.Instance.PhysicsSettings.ShipUseGravity;
		physics.isKinematic = true;//if false, ship makes small movements for reasons i do not understand

		SpaceShip ship = shipObject.AddComponent<SpaceShip> ();
		ship.Initiate (data, physics);
		shipObject.transform.SetParent (_player.transform);

		_shipInConstruction = ship;
	}

	public void EditShip ()
	{
		_shipInConstruction.Physics.isKinematic = true;
		if (_player.SelectedStructure is SpaceShip)
			_shipInConstruction = _player.SelectedStructure as SpaceShip;

		_selectedPosition = new Vector3Int (0, 0, 0);

		_shipInConstruction.transform.position = transform.position;
		_shipInConstruction.transform.rotation = transform.rotation;
	}

	public void StartAddingPart (Part newPart)
	{
		if (_selectedPart != null) //safety check in case new adding is started before the old one is done
			Destroy (_selectedPart.gameObject);



		_selectedPart = Instantiate (newPart.gameObject, _selectedPosition, Quaternion.Euler (0, 0, 0), _shipInConstruction.transform).GetComponent<Part> ();
	}

	public void FinishAddingPart ()
	{
		_shipInConstruction.Data.AddPart (_selectedPart, _selectedPosition);

		_selectedPart = Instantiate (_selectedPart.gameObject, _selectedPosition, Quaternion.Euler (0, 0, 0), _shipInConstruction.transform).GetComponent<Part> ();
	}

	public void FinishCreation ()
	{
		_player.AddStructure (_shipInConstruction);

		_shipInConstruction.Physics.isKinematic = false;
	}

	public void FinishEditing ()
	{
		_shipInConstruction.Physics.isKinematic = false;
	}



	private void MoveSelectedPosition ()
	{
		if (Input.GetKeyDown (_settings.RightKey) && _selectedPosition.x < _dimensions.x)
			_selectedPosition.x++;
		if (Input.GetKeyDown (_settings.LeftKey) && _selectedPosition.x > 0)
			_selectedPosition.x--;
		if (Input.GetKeyDown (_settings.UpKey) && _selectedPosition.y < _dimensions.y)
			_selectedPosition.y++;
		if (Input.GetKeyDown (_settings.DownKey) && _selectedPosition.y > 0)
			_selectedPosition.y--;
		if (Input.GetKeyDown (_settings.ForwardKey) && _selectedPosition.z < _dimensions.z)
			_selectedPosition.z++;
		if (Input.GetKeyDown (_settings.BackwardsKey) && _selectedPosition.z > 0)
			_selectedPosition.z--;

		//print (_selectedPosition);

		if (_selectedPart != null)
			_selectedPart.transform.position = _selectedPosition;
	}

	private void CheckFinishKey ()
	{
		if (Input.GetKeyDown (_settings.FinishKey))
			FinishAddingPart ();
	}

	private void CheckDeleteKey ()
	{
		if (Input.GetKeyDown (_settings.DeleteKey))
		{
			/*
			if (_ship [_selectedPosition.x, _selectedPosition.y, _selectedPosition.z] != null)
				Destroy (_ship [_selectedPosition.x, _selectedPosition.y, _selectedPosition.z].gameObject);
				*/

			Destroy (_selectedPart.gameObject);
		}
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerHuman : Player
{
	public event EventHandler<StructureArgs> OnSelectedStructureChanged;

	public Structure SelectedStructure {get{return _selectedStructure;}}
	public Structure SelectedTarget {get {return _selectedTarget;}}
	public TurretGroup SelectedTurretGroup {get {return _selectedTurretGroup;}}

	protected Structure _selectedStructure;
	protected Structure _selectedTarget;
	protected TurretGroup _selectedTurretGroup;

	//TODO Input settings are hard-coded for testing. Add ability to set it from inside the game later
	private KeyCode _shootKey = KeyCode.Space;
	private KeyCode _moveForward = KeyCode.W;
	private KeyCode _rotatePositiveX = KeyCode.UpArrow;
	private KeyCode _rotateNegativeX = KeyCode.DownArrow;
	private KeyCode _rotatePositiveY = KeyCode.D;
	private KeyCode _rotateNegativeY = KeyCode.A;
	private KeyCode _rotatePositiveZ = KeyCode.LeftArrow;
	private KeyCode _rotateNegativeZ = KeyCode.RightArrow;

	private bool _isControlsWorking = true;



	protected override void Awake ()
	{
		base.Awake ();

		if (_selectedStructure == null && _controlledStructures.Count > 0)
			_selectedStructure = _controlledStructures [0];
	}

	protected override void Start ()
	{
		base.Start ();
	}

	protected override void Update ()
	{
		base.Update ();

		if (_isControlsWorking)
		{
			if (Input.GetKey (_shootKey)) {
				Fire (_selectedTarget);
			}

			if (Input.GetKey (_moveForward)) {
				MoveForward ();
			}

			Rotate ();
		}
	}



	public override void AddStructure (Structure structure)
	{
		base.AddStructure (structure);

		if (_selectedStructure == null)
			_selectedStructure = structure;
	}

	public void SelectTurretGroup (TurretGroup selected)
	{
		_selectedTurretGroup = selected;
	}

	public void SelectTarget (Structure target)
	{
		_selectedTarget = target;
	}



	private void Aim (Transform target)
	{
		_selectedTurretGroup.Aim (target);
	}

	private void Fire (Structure target)
	{


		if (_selectedTurretGroup == null)
			return;

		_selectedTurretGroup.Fire (target);
	}

	private void MoveForward ()
	{
		if (_selectedStructure is SpaceShip)
		{
			(_selectedStructure as SpaceShip).MoveForward ();
		}
	}

	private void Rotate ()
	{
		Vector3 rotationDirections = new Vector3 (0, 0, 0);

		if (Input.GetKey (_rotatePositiveX))
			rotationDirections.x += 1;
		if (Input.GetKey (_rotateNegativeX))
			rotationDirections.x -= 1;
		if (Input.GetKey (_rotatePositiveY))
			rotationDirections.y += 1;
		if (Input.GetKey (_rotateNegativeY))
			rotationDirections.y -= 1;
		if (Input.GetKey (_rotatePositiveZ))
			rotationDirections.z += 1;
		if (Input.GetKey (_rotateNegativeZ))
			rotationDirections.z -= 1;

		if (_selectedStructure is SpaceShip)
			(_selectedStructure as SpaceShip).Rotate (rotationDirections);
	}
}

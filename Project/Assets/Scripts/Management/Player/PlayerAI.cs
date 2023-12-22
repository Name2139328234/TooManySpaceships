using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerAI : Player
{
	[SerializeField]
	private float _acceptableShootingAngle;
	[SerializeField]
	private float _acceptableMovingAngle;
	[SerializeField]
	private float _desiredDistanceToTarget;

	[SerializeField]//TODO testing. remove later
	private List<Structure> _enemies;
	private GameObject _aimAssistant;



	protected override void Awake ()
	{
		base.Awake ();
	}

	protected override void Start ()
	{
		base.Start ();

		_aimAssistant = new GameObject ("Aim Assistant");
		_aimAssistant.transform.SetParent (transform);

		_enemies = new List<Structure> ();

		foreach (Structure structure in LevelInfo.Instance.LoadedStructures)
		{
			if (structure.Data.Team.CheckAttitude (_team) == Relationship.Enemy)
				_enemies.Add (structure);
		}

		foreach (Structure structure in _controlledStructures)
		{
			CreateTurretGroupsIn (structure);
		}

		_team.OnDiplomacyChange += OnDiplomacyChange;
	}

	protected override void Update ()
	{
		base.Update ();

		InterractWithEnemies ();
	}

	private void InterractWithEnemies ()
	{
		if (_enemies.Capacity == 0)
			return;
		
		foreach (Structure structure in _controlledStructures)
		{
			Structure nearestEnemy = _enemies[0];

			foreach (Structure enemyStructure in _enemies)
			{
				if (Vector3.Distance (structure.transform.position, enemyStructure.transform.position) < Vector3.Distance (structure.transform.position, nearestEnemy.transform.position))
					nearestEnemy = enemyStructure;
			}

			if (structure is SpaceShip)
			{
				RotateShipTowardsTarget (structure as SpaceShip, nearestEnemy.gameObject);

				_aimAssistant.transform.position = structure.transform.position;
				_aimAssistant.transform.LookAt (nearestEnemy.transform);

				if (IsAngleAcceptable (structure.transform.forward, _aimAssistant.transform.forward) && Vector3.Distance (nearestEnemy.transform.position, structure.transform.position) > _desiredDistanceToTarget)
					MoveForward (structure as SpaceShip);


			}

			FireGunsFromAt (structure, nearestEnemy);
		}
	}

	private void OnDiplomacyChange (object sender, DiplomacyChangeArgs e)
	{
		foreach (Structure structure in LevelInfo.Instance.LoadedStructures)
		{
			if (structure.Data.Team.CheckAttitude (_team) == Relationship.Enemy)
				_enemies.Add (structure);
		}
	}

	private bool IsAngleAcceptable (Vector3 angle1, Vector3 angle2)//TODO library
	{
		return Vector3.Angle (angle1, angle2) < _acceptableMovingAngle;
	}

	private void FireGunsFromAt (Structure shooter, Structure target)
	{
		foreach (TurretGroup turretGroup in shooter.TurretGroups)
		{
			turretGroup.Aim (target.transform);
			turretGroup.Fire (target);
		}
	}

	private void CreateTurretGroupsIn (Structure structure)
	{
		List<Turret> turrets = new List<Turret> ();

		foreach (Part part in structure.Data.Parts)
		{
			if (part is Turret)
				turrets.Add (part as Turret);
		}

		TurretGroup turretGroup = structure.gameObject.AddComponent<TurretGroup> ();
		turretGroup.Initiate ("all guns", turrets.ToArray ());

		structure.AddTurretGroup (turretGroup);
	}

	private void RotateShipTowardsTarget (SpaceShip ship, GameObject target)//TODO library
	{
		_aimAssistant.transform.position = ship.transform.position;
		_aimAssistant.transform.LookAt (target.transform);

		Vector3 desiredAngle = AnglesToRange (_aimAssistant.transform.eulerAngles, 0f, 360f);
		Vector3 currentAngle = AnglesToRange (ship.transform.eulerAngles, 0f, 360f);

		Vector3 rotationDirections = new Vector3 (1f, 1f, 1f);

		rotationDirections.x *= Mathf.Sign ((desiredAngle.x - currentAngle.x + 540) % 360 - 180);
		rotationDirections.y *= Mathf.Sign ((desiredAngle.y - currentAngle.y + 540) % 360 - 180);
		rotationDirections.z *= Mathf.Sign ((desiredAngle.z - currentAngle.z + 540) % 360 - 180);

		ship.Rotate (rotationDirections); //TODO make it rotate using physics instead. Second opinion: to hell with rotation physics, just make everything rotate like that
	}

	private void MoveForward (SpaceShip ship)
	{
		if (ship is SpaceShip)
		{
			(ship as SpaceShip).MoveForward ();
		}
	}

	private Vector3 AnglesToRange (Vector3 angles, float bottom, float top)//TODO library
	{
		while (angles.x > top)
			angles.x -= 360;
		while (angles.x < bottom)
			angles.x += 360;

		while (angles.y > top)
			angles.y -= 360;
		while (angles.y < bottom)
			angles.y += 360;

		while (angles.z > top)
			angles.z -= 360;
		while (angles.z < bottom)
			angles.z += 360;


		return angles;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ShipData : StructureData
{
	public float Speed
	{
		get
		{
			if (_mass == 0)
				return 0;
			else
				return _enginePower / _mass;
		}
	}
	public float AngularSpeed
	{
		get
		{
			if (_mass == 0)
				return 0;
			else
				return _maneuverPower / _mass;
		}
	}

	private float _enginePower = 0;
	private float _maneuverPower = 0;



	protected override void Start ()
	{
		base.Start ();

		foreach (Part part in _parts)
		{
			if (part is Engine == false)
				continue;

			Engine engine = part as Engine;
				
			_enginePower += engine.MotorPower;
			_maneuverPower += engine.ManeuverPower;

			engine.OnDestroy += Engine_OnDestroy;
		}
	}



	public override void AddPart (Part part, Vector3Int place)
	{
		base.AddPart (part, place);

		if (part is Engine)
		{
			Engine engine = part as Engine;

			_enginePower += engine.MotorPower;
			_maneuverPower += engine.ManeuverPower;

			engine.OnDestroy += Engine_OnDestroy;
		}
	}



	private void Engine_OnDestroy (object sender, System.EventArgs e)
	{
		_enginePower -= (sender as Engine).MotorPower;
		_maneuverPower -= (sender as Engine).ManeuverPower;
	}
}

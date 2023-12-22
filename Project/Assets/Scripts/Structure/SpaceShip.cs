using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : Structure
{



	public void MoveForward ()
	{
		_physics.AddForce (transform.forward * (_data as ShipData).Speed * Time.deltaTime, ForceMode.Impulse);
	}


	public void Rotate (Vector3 directions) 
	{
		transform.Rotate (directions.normalized * (_data as ShipData).AngularSpeed * Time.deltaTime);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serializer : Singleton<Serializer>
{
	public string SerializeShip (SpaceShip ship)
	{
		string result = "";

		result += SerializeStructure (ship);

		return result;
	}

	private string SerializeStructure (Structure structure)
	{
		string result = "";



		return result;
	}
}

using System;
using UnityEngine;



public class GameObjectArgs : EventArgs
{
	public GameObject Object;

	public GameObjectArgs (GameObject obj) //can't use "object" because it is already used by C#
	{
		Object = obj;
	}
}

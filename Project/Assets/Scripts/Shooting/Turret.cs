using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Turret : Part
{
	[SerializeField]
	protected Shooter[] _shooters;



	public void Fire (Structure target)//TODO bullets do not inherit shooter's inertia and i can find any good solution to this
	{
		foreach (Shooter shooter in _shooters)
			shooter.Shoot (target);
	}
}

using System;



public class PartArgs : EventArgs
{

	public Health Health {get{return _health;}}
	public float Mass {get{return _mass;}}
	//public float EnergyConsumption {get{return _energyConsumption;}}

	protected Health _health;
	protected float _mass;
	//protected float _energyConsumption;


	public PartArgs (Part part)
	{
		_health = part.Health;
		_mass = part.Mass;
		//_energyConsumption = part.EnergyConsumption;
	}
}
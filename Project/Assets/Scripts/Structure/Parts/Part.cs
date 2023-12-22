using System;
using UnityEngine;



public class Part : MonoBehaviour 
{
	public event EventHandler<EventArgs> OnDestroy;

	public Health Health {get{return _health;}}
	public float Mass {get{return _mass;}}
	//public float EnergyConsumption {get{return _energyConsumption;}}
	public float Cost {get{return _cost;}}

	[SerializeField]
	protected Health _health;
	[SerializeField]
	protected float _mass;
	/*[SerializeField]
	protected float _energyConsumption;*/
	[SerializeField]
	private float _cost;



	protected virtual void Start ()
	{
		_health.OnDie += Health_OnDie;
	}




	protected virtual void Health_OnDie (object sender, EventArgs e)
	{
		if (OnDestroy != null)
			OnDestroy (this, new PartArgs (this));

		Destroy (gameObject);
	}

	protected void CallOnDestroyInHeir (object sender, PartArgs e) //TODO why does this exist?
	{
		if (OnDestroy != null)
			OnDestroy (sender, e);
	}
}
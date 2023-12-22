using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class Structure : MonoBehaviour
{
	public event EventHandler<EventArgs> OnDeath;
	public event EventHandler<TurretGroupArgs> OnAddedTurretGroup;

	public Rigidbody Physics {get{return _physics;}}
	public StructureData Data {get{return _data;}}
	public TurretGroup[] TurretGroups {get{return _turretGroups.ToArray ();}}

	[SerializeField]
	protected Rigidbody _physics;
	[SerializeField]
	protected StructureData _data;
	[SerializeField]
	protected List<TurretGroup> _turretGroups;



	protected virtual void Awake ()
	{
		//if (_data != null)
			//_data.OnHealthChange += Data_OnHealthChange;
		//_turretGroups = new List<TurretGroup> ();
	}



	public void Initiate (StructureData data, Rigidbody physics)
	{
		_data = data;

		_data.OnHealthChange += Data_OnHealthChange;

		_physics = physics;
	}

	public void AddTurretGroup (TurretGroup added)
	{
		_turretGroups.Add (added);

		if (OnAddedTurretGroup != null)
			OnAddedTurretGroup (this, new TurretGroupArgs (added));
	}



	private void Data_OnHealthChange (object sender, EventArgsFloat e)
	{
		if (_data.Health <= 0)
		{
			if (OnDeath != null)
				OnDeath (this, new EventArgs ());

			Destroy (gameObject);
		}
	}
}

using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class Player : MonoBehaviour 
{
	public Structure[] ControlledStructures {get{return _controlledStructures.ToArray ();}}
	public Team Team {get{return _team;}}

	[SerializeField]//TODO remove this and load that data from data saves
	protected List<Structure> _controlledStructures;
	[SerializeField]
	protected Team _team;



	protected virtual void Awake ()
	{
		foreach (Structure structure in _controlledStructures)
		{
			structure.OnDeath += Structure_OnDeath;
		}
	}

	protected virtual void Start ()
	{

	}

	protected virtual void Update ()
	{
		
	}



	public virtual void AddStructure (Structure structure)
	{
		_controlledStructures.Add (structure);
	}



	private void Structure_OnDeath (object sender, EventArgs e)
	{
		_controlledStructures.Remove (sender as Structure);
	}
}

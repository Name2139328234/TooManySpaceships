using System;
using System.Collections.Generic;
using UnityEngine;



public abstract class StructureData : MonoBehaviour
{
	public event EventHandler<EventArgsFloat> OnHealthChange;

	public Part[] Parts
	{
		get
		{
			List<Part> parts = new List<Part> ();

			foreach (Part part in _parts)
			{
				if (part != null)
					parts.Add (part);
			}

			return parts.ToArray ();
		}
	}

	public Vector3 Dimensions {get{return _dimensions;}}
	public float MaxHealth {get{return _maxHealth;}}
	public float Health {get{return _health;}}
	/*public float MaxEnergy {get{return _maxEnergy;}}
	public float Energy {get{return _energy;}}
	public float EnergyGeneration {get{return _energyGeneration;}}
	public float EnergyConsumption {get{return _energyConsumption;}}*/
	public float Mass {get{return _mass;}}
	public Team Team {get{return _team;}}
	public string Name {get{return _name;}}

	[SerializeField]
	protected string _name;
	[SerializeField]
	protected Team _team;

	protected Part[,,] _parts;
	protected ControlCenter _root;
	protected Vector3 _dimensions;
	protected float _maxHealth = 0;
	protected float _health = 0;
	/*protected float _maxEnergy = 0;
	protected float _energy = 0;
	protected float _energyGeneration = 0;
	protected float _energyConsumption = 0;*/
	protected float _mass = 0;



	protected virtual void Start ()
	{
	}



	public virtual void AddPart (Part part, Vector3Int place)
	{
		if (_parts [place.x, place.y, place.z] == null)
		{
			if (part is ControlCenter)
			{
				if (_root == null)
					_root = part as ControlCenter;
				else
					Debug.LogError ("two root pats detected");
			}

			_parts [place.x, place.y, place.z] = part;

			part.transform.localPosition = place;

			_maxHealth += part.Health.MaxValue;
			_health += part.Health.Value;
			_mass += part.Mass;

			part.OnDestroy += Part_OnDestroy;
			part.Health.OnChangeValue += UpdateHealth;
		}
		else
		{
			print ("place of part is already occupied");//TODO replace part or show in-game message that the place is occupied
		}
	}

	public void Initiate (Team team, String name, Vector3Int dimensions)
	{
		_parts = new Part[dimensions.x, dimensions.y, dimensions.z];

		_team = team;
		_name = name;
		_dimensions = dimensions;
	}



	private void Part_OnDestroy (object sender, EventArgs e)
	{
		_maxHealth -= (sender as Part).Health.MaxValue;
		//_energyConsumption -= e.EnergyConsumption;
		_mass -= (sender as Part).Mass;

		/*foreach (Part part in _parts)
		{
			if (part == sender as Part)
				part = null; //TODO will this work?
		}*/

		for (int x = 0; x < _dimensions.x; x++)
		{
			for (int y = 0; y < _dimensions.y; y++)
			{
				for (int z = 0; z < _dimensions.z; z++)
				{
					if (_parts[x, y, z] == sender as Part)
						_parts[x, y, z] = null;//TODO learn how break keyword works in multiple cycles
				}
			}
		}
	}

	private void UpdateHealth (object sender, EventArgsFloat e)
	{
		_health += e.FloatArg;

		if (OnHealthChange != null)
			OnHealthChange (this, e);
	}
}
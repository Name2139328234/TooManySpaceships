using System;
using System.Collections.Generic;
using UnityEngine;



public class Team : MonoBehaviour
{
	public event EventHandler<DiplomacyChangeArgs> OnDiplomacyChange;

	public string Name {get{return _name;}}

	[SerializeField]
	private string _name;

	[SerializeField]//TODO testing. remove later
	private Team[] _enemies;

	private Dictionary <Team, Relationship> _diplomacy;//TODO information about team relationship is stored in two places independently. Rework is required



	void Awake ()
	{
		_diplomacy = new Dictionary<Team, Relationship> ();
	}

	void Start ()
	{
		foreach (Team enemy in _enemies)
			SetAttitude (enemy, Relationship.Enemy);
	}

	public Relationship CheckAttitude (Team target)
	{

		if (target.Name == _name)
			return Relationship.Ally;

		Relationship result;

		if (_diplomacy.ContainsKey (target) == false)
			_diplomacy.Add (target, Relationship.Neutral);

		_diplomacy.TryGetValue (target, out result);
		return result;
	}

	public void SetAttitude (Team target, Relationship value)
	{
		if (_diplomacy.ContainsKey (target))
			_diplomacy [target] = value;
		else
			_diplomacy.Add (target, value);

		if (target.CheckAttitude (this) != value)
			target.SetAttitude (this, value);

		if (OnDiplomacyChange != null)
			OnDiplomacyChange (this, new DiplomacyChangeArgs (target, value));
	}
}

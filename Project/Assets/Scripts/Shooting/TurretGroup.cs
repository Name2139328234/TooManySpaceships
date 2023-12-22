using System.Collections.Generic;
using UnityEngine;



public class TurretGroup : MonoBehaviour
{
	public string Name {get{return _name;}}
	public Turret[] Members {get{return _members.ToArray ();}}
	public bool Track {get{return _track;}}
	public bool FireAtWill {get{return _fireAtWill;}}

	[SerializeField]
	private string _name;
	[SerializeField]
	private List<Turret> _members;
	[SerializeField]
	private bool _track;
	[SerializeField]
	private bool _fireAtWill;

	private Structure _lastTarget;



	void Start ()
	{
		foreach (Turret turret in _members)
			turret.OnDestroy += DeleteDeadMember;
	}

	void Update ()
	{
		if (_fireAtWill)
		{
			Fire (_lastTarget);
		}

		if (_track && _lastTarget != null)
		{
			Aim (_lastTarget.transform);
		}
	}

	public void Initiate (string name, Turret[] group)
	{
		Initiate (name, group, false);
	}

	public void Initiate (string name, Turret[] group, bool fireAtWill)
	{
		_members = new List<Turret> ();

		_name = name;
		_members.AddRange (group);
		_fireAtWill = fireAtWill;
	}

	public void Aim (Transform target)
	{
		foreach (Turret turret in _members)
		{
			if (turret is BallTurret)
				(turret as BallTurret).Aim (target.transform);
		}

		Structure targetStructure = target.GetComponent<Structure> ();

		if (targetStructure != null)
			_lastTarget = targetStructure;
	}

	public void Fire (Structure target)
	{
		_lastTarget = target;

		foreach (Turret turret in _members)
			turret.Fire (target);
	}

	public void SetFireAtWill (bool fire)
	{
		_fireAtWill = fire;
	}

	public void SetTracking (bool track)
	{
		_track = track;
	}

	private void DeleteDeadMember (object sender, System.EventArgs e)
	{
		_members.Remove (sender as Turret);
	}
}

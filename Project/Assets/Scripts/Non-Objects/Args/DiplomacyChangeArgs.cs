using System;



public class DiplomacyChangeArgs : EventArgs
{
	public Team ChangeTarget {get{return _changeTarget;}}
	public Relationship NewRelationship {get{return _newRelationship;}}

	private Team _changeTarget;
	private Relationship _newRelationship;



	public DiplomacyChangeArgs (Team changeTarget, Relationship newRelationship)
	{
		_changeTarget = changeTarget;
		_newRelationship = newRelationship;
	}
}

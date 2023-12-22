using System;



public class TurretGroupArgs : EventArgs
{
	public TurretGroup Group {get{return _group;}}

	private TurretGroup _group;



	public TurretGroupArgs (TurretGroup group)
	{
		_group = group;
	}
}

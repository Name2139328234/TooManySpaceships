using System;



public class StructureListUpdateArgs : EventArgs
{
	public Structure[] Added {get{return _added;}}
	public Structure[] Removed {get {return _removed;}}

	private Structure[] _added;
	private Structure[] _removed;



	public StructureListUpdateArgs (Structure[] added, Structure[] removed)
	{
		_added = added;
		_removed = removed;
	}
}

using System;



public class StructureArgs : EventArgs
{
	public Structure Structure {get{return _structure;}}

	private Structure _structure;



	public StructureArgs (Structure structure)
	{
		_structure = structure;
	}
}

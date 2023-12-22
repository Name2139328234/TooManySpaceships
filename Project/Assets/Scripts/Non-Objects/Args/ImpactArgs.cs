using System;



public class ImpactArgs : EventArgs
{
	public Impact Impact;

	public ImpactArgs (Impact impact)
	{
		Impact = impact;
	}
}

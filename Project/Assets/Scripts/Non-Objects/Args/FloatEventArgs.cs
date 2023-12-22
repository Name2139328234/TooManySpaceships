using System;



public class EventArgsFloat : EventArgs
{
	public float FloatArg;



	public EventArgsFloat (float floatArg) : base ()
	{
		this.FloatArg = floatArg;
	}
}
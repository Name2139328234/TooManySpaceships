


public class EngineArgs : PartArgs
{
	public float EnginePower {get{return _enginePower;}}

	private float _enginePower;



	public EngineArgs (Engine engine) : base (engine)
	{
		_enginePower = engine.MotorPower;
	}
}
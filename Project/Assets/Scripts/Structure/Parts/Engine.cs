using UnityEngine;


public class Engine : Part 
{
	public float MotorPower {get{return _motorPower;}}
	public float ManeuverPower {get {return _maneuverPower;}}

	[SerializeField]
	private float _motorPower;
	[SerializeField]
	private float _maneuverPower;



	protected override void Start ()
	{
		_health.OnDie += Health_OnDie;
	}

	protected override void Health_OnDie (object sender, System.EventArgs e)
	{
		CallOnDestroyInHeir (this, new EngineArgs (this));

		Destroy (gameObject);
	}
}
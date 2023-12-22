using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile
{
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _acceleration;
	[SerializeField]
	private float _accelerationTime;

	private Timer _accelerationTimer;



	protected override void Start ()
	{
		base.Start ();

		_accelerationTimer = Timer.New (_accelerationTime, false);
	}

	protected override void Update ()
	{
		base.Update ();

		Move ();
		Accelerate ();
	}

	private void Move ()
	{
		transform.position += _direction * _speed * Time.deltaTime;
	}

	private void Accelerate ()
	{
		if (_accelerationTimer.IsOver == false)
		{
			_speed += _acceleration * Time.deltaTime;
		}
	}
}

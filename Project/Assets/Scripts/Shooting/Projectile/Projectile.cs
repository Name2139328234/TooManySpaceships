using System;
using UnityEngine;



public abstract class Projectile : MonoBehaviour
{
	[SerializeField]
	protected Impact _impact;
	[SerializeField]
	protected float _lifeTime;

	/// <summary>
	/// The target of that projectile. Can be null
	/// </summary>
	protected Structure _target;

	protected Vector3 _direction;
	protected Shooter _shooter;
	protected Timer _lifeTimer;



	protected virtual void Start ()
	{
		_lifeTimer = Timer.New (_lifeTime, false);
		_lifeTimer.OnTimeRanOut += Expire;
	}

	protected virtual void Update ()
	{
		
	}

	protected virtual void OnTriggerEnter (Collider other)
	{
		
	}



	public void Initiate (Shooter shooter, Vector3 direction, Structure target)
	{
		PrivateInitiate (shooter, direction, target);
	}



	protected virtual void RegisterHit (Part target)
	{
		if (_shooter == null)
			return; //necessary safety feature in case the shooter is destroyed before the projectile reaches the target. TODO find a way to remove

		Structure targetStructure = FindComponentInYoungestParent<Structure> (target.transform);

		Structure shooterStructure = FindComponentInYoungestParent<Structure> (_shooter.transform);//TODO it is not guaranteed, that Shooter has a structure as parent. Potential solution: tie Shooter to Turret with RequireComponent, so they will always stay at the same gameobject

		if (GameRules.Instance.DamageRules.CanDamageBeDone (shooterStructure, targetStructure) == false)
			return;

		_impact.Apply (target.Health);
	}

	protected virtual void PrivateInitiate (Shooter shooter, Vector3 direction, Structure target) //this is needed for correct inheritance
	{
		_shooter = shooter;
		_direction = direction;
		_target = target;
	}

	protected virtual void Expire (object sender, EventArgs e)
	{
		Destroy (gameObject);
	}

	protected T FindComponentInYoungestParent<T> (Transform child) where T : MonoBehaviour //TODO either add to my library or learn whether this is what GetComponentInParent does
	{
		T result = null;

		while (result == null && child.parent != null)
		{
			result = child.GetComponent<T> ();

			child = child.parent;
		}

		return result;
	}
}
using System;
using UnityEngine;



public class Impact : MonoBehaviour
{
	public event EventHandler<EventArgs> OnImpact;

	public float Damage {get {return _damage;}}

	[SerializeField]
	private float _damage;



	public void Apply (Health target)
	{
		if (OnImpact != null)
			OnImpact (this, new ImpactArgs (this));

		target.TakeDamage (_damage);
	}
}

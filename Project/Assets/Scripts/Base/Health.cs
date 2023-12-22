using System;
using UnityEngine;



public class Health : MonoBehaviour
{
	public event EventHandler<EventArgsFloat> OnTakeDamage;
	public event EventHandler<EventArgsFloat> OnChangeValue;
	public event EventHandler<EventArgs> OnDie;

	public float Value {get{return _value;}}
	public float MaxValue {get{return _maxValue;}}
	public float NormalizedValue {get{return _value / _maxValue;}}

	[SerializeField]
	private float _maxValue;

	private float _value;



	void Start ()
	{
		_value = _maxValue;

		if (OnChangeValue != null) OnChangeValue (this, new EventArgsFloat (_value));
	}



	public void TakeDamage (float damage)
	{
		if (_value == 0) return;
		if (damage == 0) return;

		if (_value - damage < 0)
			damage = _value;
		
		_value -= damage;

		if (OnTakeDamage != null) OnTakeDamage (this, new EventArgsFloat (damage));
		if (OnChangeValue != null) OnChangeValue (this, new EventArgsFloat (-1 * damage));

		if (_value <= 0)
		{
			Destroy (gameObject);

			if (OnDie != null) OnDie (this, null);
		}
	}

	public void TakeHeal (float heal)
	{
		if (heal == 0) return;

		if (_value + heal > _maxValue)
			heal = _maxValue - _value;
		
		_value += heal;

		if (OnChangeValue != null) OnChangeValue (this, new EventArgsFloat (heal));
	}
}
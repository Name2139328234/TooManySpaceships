using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsSettings : MonoBehaviour
{
	public float ShipDrag {get{return _shipDrag;}}
	public float ShipAngularDrag {get{return _shipAngularDrag;}}
	public bool ShipUseGravity {get{return _shipUseGravity;}}

	[SerializeField]
	private float _shipDrag;
	[SerializeField]
	private float _shipAngularDrag;
	[SerializeField]
	private bool _shipUseGravity;
}

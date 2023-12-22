using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTurret : Turret
{
	[SerializeField]
	private Transform _rotatingPart;
	[SerializeField]
	private Vector3 _aimAngleLockTop;
	[SerializeField]
	private Vector3 _aimAngleLockBottom;

	public void Aim (Transform target)
	{
		_rotatingPart.transform.LookAt (target);
		Vector3 newAngles = _rotatingPart.transform.localRotation.eulerAngles;

		newAngles = AnglesToRange (newAngles, -180f);

		newAngles.x = Mathf.Clamp (newAngles.x, _aimAngleLockBottom.x, _aimAngleLockTop.x);
		newAngles.y = Mathf.Clamp (newAngles.y, _aimAngleLockBottom.y, _aimAngleLockTop.y);
		newAngles.z = Mathf.Clamp (newAngles.z, _aimAngleLockBottom.z, _aimAngleLockTop.z);

		_rotatingPart.transform.localRotation = Quaternion.Euler (newAngles);
	}

	public bool CanLookAt (Transform target)
	{
		Vector3 oldRotation = _rotatingPart.transform.eulerAngles;

		_rotatingPart.transform.LookAt (target);
		Vector3 atTargetRotation = _rotatingPart.transform.eulerAngles;

		Aim (target);
		Vector3 restrictedRotation = _rotatingPart.transform.eulerAngles;

		_rotatingPart.transform.eulerAngles = oldRotation;

		return atTargetRotation == restrictedRotation;
	}

	private Vector3 AnglesToRange (Vector3 angles, float rangeBottom)
	{
		float rangeTop = rangeBottom + 360;

		while (angles.x < rangeBottom)
			angles.x += 360;
		while (angles.x > rangeTop)
			angles.x -= 360;
		
		while (angles.y < rangeBottom)
			angles.y += 360;
		while (angles.y > rangeTop)
			angles.y -= 360;

		while (angles.z < rangeBottom)
			angles.z += 360;
		while (angles.z > rangeTop)
			angles.z -= 360;

		return angles;
	}
}

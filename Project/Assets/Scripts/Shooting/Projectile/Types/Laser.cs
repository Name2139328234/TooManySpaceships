using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Projectile
{
	[SerializeField]
	private float _length;

	protected override void Start ()
	{
		base.Start ();

		AdjustViewToLength ();

		CastRay ();
	}



	private void CastRay ()
	{
		RaycastHit hitInfo;

		Physics.Raycast (new Ray (_shooter.transform.position, _direction), out hitInfo, _length);

		Part part = hitInfo.collider.GetComponent<Part> ();

		if (part == null)
			part = hitInfo.collider.GetComponentInParent<Part> ();

		if (part == null)
			part = hitInfo.collider.GetComponentInChildren<Part> ();

		if (part != null)
			RegisterHit (part);

		Destroy (gameObject);
	}

	private void AdjustViewToLength ()
	{
		//TODO make lasers look like something
	}
}

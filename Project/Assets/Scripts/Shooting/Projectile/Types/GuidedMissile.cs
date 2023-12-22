using UnityEngine;



public class GuidedMissile : Projectile 
{
	[SerializeField]
	private Rigidbody _physics;
	[SerializeField]
	private float _speed;
	[SerializeField]
	private float _angularSpeed;
	[SerializeField]
	private float _acceptableMovingAngle;
	//[SerializeField]
	//private float _explosionDistanceToTarget; //TODO add explosions

	private GameObject _followGroup;
	private GameObject _aimAssistant;



	protected override void Start ()
	{
		base.Start ();

		_followGroup = new GameObject ("Follow Group");
		_aimAssistant = new GameObject ("Aim Assistant");
		_aimAssistant.AddComponent<Follow> ().Initiate (transform);

		transform.SetParent (_followGroup.transform);
		_aimAssistant.transform.SetParent (_followGroup.transform);
	}

	protected override void Update ()
	{
		base.Update ();

		RotateTowardsTarget ();
		Move ();
	}

	protected override void OnTriggerEnter (Collider other)
	{
		base.OnTriggerEnter (other);

		Part part = other.GetComponent<Part> ();

		if (part == null)
			part = other.GetComponentInParent<Part> ();

		if (part == null)
			part = other.GetComponentInChildren<Part> ();

		if (part != null)
			RegisterHit (part);

		_lifeTimer.OnTimeRanOut -= Expire;

		Destroy (_followGroup);
	}



	private void RotateTowardsTarget ()
	{
		if (_target != null)
			_aimAssistant.transform.LookAt (_target.transform);

		Vector3 desiredAngle = AnglesToRange (_aimAssistant.transform.eulerAngles, 0f, 360f);
		Vector3 currentAngle = AnglesToRange (transform.eulerAngles, 0f, 360f);

		Vector3 rotation = new Vector3 (_angularSpeed * Time.deltaTime, _angularSpeed * Time.deltaTime, _angularSpeed * Time.deltaTime);

		rotation.x *= Mathf.Sign ((desiredAngle.x - currentAngle.x + 540) % 360 - 180);
		rotation.y *= Mathf.Sign ((desiredAngle.y - currentAngle.y + 540) % 360 - 180);
		rotation.z *= Mathf.Sign ((desiredAngle.z - currentAngle.z + 540) % 360 - 180);

		transform.Rotate (rotation);//TODO make it rotate using physics instead
	}

	private void Move ()
	{
		if (IsAngleAcceptable (_aimAssistant.transform.eulerAngles, transform.eulerAngles))
			_physics.AddForce (transform.forward * _speed * Time.deltaTime, ForceMode.Impulse);
	}

	private Vector3 AnglesToRange (Vector3 angles, float bottom, float top)//TODO make my library with those functions
	{
		while (angles.x > top)
			angles.x -= 360;
		while (angles.x < bottom)
			angles.x += 360;

		while (angles.y > top)
			angles.y -= 360;
		while (angles.y < bottom)
			angles.y += 360;

		while (angles.z > top)
			angles.z -= 360;
		while (angles.z < bottom)
			angles.z += 360;
		

		return angles;
	}

	private bool IsAngleAcceptable (Vector3 angle1, Vector3 angle2)//TODO library
	{
		return Vector3.Angle (angle1, angle2) < _acceptableMovingAngle;
	}
}

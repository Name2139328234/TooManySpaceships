using UnityEngine;



public class Bullet : Projectile
{
	[SerializeField]
	private float _speed;

	protected override void Update ()
	{
		base.Update ();

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

		Destroy (gameObject);
	}



	private void Move ()
	{
		transform.position += _direction * _speed * Time.deltaTime;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRorartion : MonoBehaviour
{
	[SerializeField]
	private Camera _camera;
	[SerializeField]
	private float _speed;
	[SerializeField]
	private KeyCode _zRotationPos;
	[SerializeField]
	private KeyCode _zRotationNeg;



	void Start ()
	{
		if (_camera == null)
			_camera = Camera.main;
	}

	void Update ()
	{
		float zRotation = 0;

		if (Input.GetKey (_zRotationPos))
			zRotation++;
		if (Input.GetKey (_zRotationNeg))
			zRotation--;

		_camera.transform.Rotate (new Vector3 (-1 * Input.GetAxis ("Mouse Y") * _speed * Time.deltaTime, Input.GetAxis ("Mouse X") * _speed * Time.deltaTime, zRotation * _speed * Time.deltaTime));
	}
}

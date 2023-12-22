using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	[SerializeField]
	private Camera _camera;
	[SerializeField]
	private float _speed;



	void Start ()
	{
		if (_camera == null)
			_camera = Camera.main;
	}

	void Update ()
	{
		_camera.transform.position += _camera.transform.forward * Input.mouseScrollDelta.y * _speed * Time.deltaTime;//.y is the delta and .x is not used. That's just the way it is
	}
}

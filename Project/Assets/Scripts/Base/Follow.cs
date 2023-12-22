using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
	//[SerializeField]
	//private Transform _self;
	//[SerializeField]
	private Transform _target;



	void Update ()
	{
		if (_target != null)
			transform.position = _target.position;
	}

	public void Initiate (Transform target)
	{
		_target = target;
	}
}

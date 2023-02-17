using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
	public Vector3 rotation;

	
	void Start()
	{

	}

	void FixedUpdate()
	{
		Rotate();
	}

	private void Rotate()
	{ 
		transform.Rotate(rotation * Time.fixedDeltaTime);
	}
}

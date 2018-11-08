using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionTest : MonoBehaviour
{
	[SerializeField] GameObject targetObject;
	[SerializeField] Rigidbody rigidbody;
	[SerializeField] float power;

	void Update()
	{
		if(Input.GetMouseButtonDown(0))
		{
			transform.LookAt(targetObject.transform);
			rigidbody.AddForce(transform.forward * power, ForceMode.Impulse);
		}
	}
}

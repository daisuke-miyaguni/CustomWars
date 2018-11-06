using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
	[SerializeField] string weaponTag;
	[SerializeField] float power;
	Rigidbody rb;
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == weaponTag)
		{
			rb = GetComponent<Rigidbody>();
			gameObject.transform.LookAt(other.gameObject.transform);
			rb.AddForce(transform.forward * (-power), ForceMode.Impulse);
		}
	}
}

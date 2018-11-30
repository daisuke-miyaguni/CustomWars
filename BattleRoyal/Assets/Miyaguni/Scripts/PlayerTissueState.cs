using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTissueState : MonoBehaviour
{
	string tissueName = "Tissue";

	[SerializeField] float fanPower;

	bool jumpState;

	Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision other)
	{
        if (transform.Find(tissueName) && jumpState)
        {
			jumpState = false;
			GameObject tissue = transform.Find(tissueName).gameObject;
			Destroy(tissue);
		}
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Fan" && transform.Find(tissueName))
		{
			rb.AddForce(transform.up * fanPower, ForceMode.Impulse);
			jumpState = true;
		}
	}
}
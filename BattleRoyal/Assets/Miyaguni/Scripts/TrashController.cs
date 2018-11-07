using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour
{
	[SerializeField] string deskTag;
    [SerializeField] string weaponTag;

    [SerializeField] float power;
    [SerializeField] float weaponPower;

    Rigidbody rb;
	
	void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag != deskTag)
		{
			rb = GetComponent<Rigidbody>();
			
			if(other.gameObject.tag == weaponTag){
				power *= weaponPower;
			}
            gameObject.transform.LookAt(other.gameObject.transform);
            rb.AddForce(transform.forward * (-power), ForceMode.Impulse);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeController : MonoBehaviour
{
    Rigidbody rb;
    string rotateYAxis = "Vertical";

	[SerializeField] float rotateSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionStay(Collision other)
    {
    	if(other.gameObject.tag == "Player")
    	{
    		// other.transform.parent = gameObject.transform;
    		// float rotateY = Input.GetAxis(rotateYAxis);

            float rotateY = Input.GetAxis(rotateYAxis) * rotateSpeed;
            rb.AddTorque(new Vector3(0, -rotateY, 0), ForceMode.Force);
            // rb.AddTorque(new Vector3(-rotateY, 0, 0), ForceMode.Force);
        }
    }

	// void OnCollisionExit(Collision other)
	// {
    //     if (other.gameObject.tag == "Player")
    //     {
    //         // other.transform.parent = null;
    //     }
	// }
}
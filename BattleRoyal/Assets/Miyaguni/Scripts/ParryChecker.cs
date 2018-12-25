using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryChecker : MonoBehaviour
{
    PlayerController playerController;
	Rigidbody rootRb;

    void Start()
    {
        playerController = gameObject.transform.root.GetComponent<PlayerController>();
		rootRb = gameObject.transform.root.GetComponent<Rigidbody>();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Parry"
        && other.gameObject.transform.root != this.gameObject.transform.root)
        {
            playerController.CallWasparryed();
            rootRb.AddForce(gameObject.transform.root.forward * -10f, ForceMode.Impulse);
            other.gameObject.GetComponent<SphereCollider>().enabled = false;
        }
    }
}

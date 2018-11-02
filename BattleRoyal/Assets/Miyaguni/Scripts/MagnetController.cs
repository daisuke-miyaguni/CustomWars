using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
	[SerializeField] float gravityPower;
    [SerializeField] string playerTag;
    GameObject player;
	Rigidbody playerRb;
	Rigidbody magnetRb;

    void Start()
	{
		player = GameObject.FindWithTag(playerTag);
		playerRb = player.GetComponent<Rigidbody>();
		magnetRb = GetComponent<Rigidbody>();
	}

	void OnTriggerStay(Collider other)
	{
		if(other.gameObject.tag == playerTag)
		{
			Vector3 direction = transform.position - other.transform.position;
			Vector3 magnetGravity = gravityPower * direction * (magnetRb.mass * playerRb.mass);
			playerRb.AddForce(magnetGravity);
		}
	}
}

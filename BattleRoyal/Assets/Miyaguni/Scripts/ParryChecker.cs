using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryChecker : MonoBehaviour
{
	PlayerController playerController;

	void Start()
	{
		playerController = gameObject.transform.parent.GetComponent<PlayerController>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == "Parry")
		{
			playerController.Parryed();
			Parry parry = other.GetComponent<Parry>();
			parry.SuccessPatty();
		}
	}
}

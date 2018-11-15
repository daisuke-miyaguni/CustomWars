using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
	[SerializeField] string playerTag;
	PhotonView photonView;

	public void Start ()
	{
		photonView = GetComponent<PhotonView>();
		photonView.enabled = false;
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.tag == playerTag)
		{
			photonView.enabled = true;
			Destroy(gameObject);
		}
	}
}
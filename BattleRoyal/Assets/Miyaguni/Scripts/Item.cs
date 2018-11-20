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
		// photonView.enabled = false;
	}

	public void OnCollisionEnter(Collision other)
	{
		if(other.gameObject.tag == playerTag)
		{
			// photonView.enabled = true;
			photonView.RPC("WasgetItem", PhotonTargets.AllViaServer);
		}
	}

	[PunRPC]
	void WasgetItem()
	{
		Destroy(gameObject);
	}
}
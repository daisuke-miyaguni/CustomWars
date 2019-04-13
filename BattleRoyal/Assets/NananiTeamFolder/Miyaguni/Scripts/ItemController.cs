using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
	PhotonView photonView;

	public void Start ()
	{
		photonView = GetComponent<PhotonView>();
	}

	public void GetItem()
	{
		photonView.RPC("WasgetItem", PhotonTargets.AllViaServer);
	}

	[PunRPC]
	void WasgetItem()
	{
		Destroy(gameObject);
	}
}
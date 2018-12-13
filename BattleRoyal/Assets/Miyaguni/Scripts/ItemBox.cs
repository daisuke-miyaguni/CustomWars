using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemBox : MonoBehaviour
{
    private enum PunRPCList
    {
        DeleteItemBox
    }

    [SerializeField] float itemSpawnPower;
    
    [SerializeField] GameObject icon;

	PhotonView photonView;

    ItemSpawner itemSpawner;

	void Start()
	{
		photonView = GetComponent<PhotonView>();
        itemSpawner = GameObject.FindWithTag("ItemSpawner").gameObject.GetComponent<ItemSpawner>();
	}
    
    // ボタンでボックスを開ける
    public void OpenOnClick()
    {
        Vector3 objectPos = this.gameObject.transform.position;
        itemSpawner.CallItemSpawn(this.gameObject, objectPos);
        photonView.RPC(PunRPCList.DeleteItemBox.ToString(), PhotonTargets.AllViaServer);
    }

	[PunRPC]
	void DeleteItemBox()
	{
		Destroy(this.gameObject);
	}
}

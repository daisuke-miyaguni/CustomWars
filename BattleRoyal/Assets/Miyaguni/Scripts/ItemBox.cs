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

    int itemCount = 3;
    [SerializeField] float itemSpawnPower;
    
    [SerializeField] GameObject icon;

    BoxCollider bc;		// 宝箱のオープン範囲
	PhotonView photonView;

    ItemSpawner itemSpawner;

	void Start()
	{
		photonView = GetComponent<PhotonView>();
		bc = GetComponent<BoxCollider>();
		itemCount = 3;
        itemSpawner = GameObject.FindWithTag("ItemSpawner").gameObject.GetComponent<ItemSpawner>();
	}
    
    // ボタンでボックスを開ける
    public void OpenOnClick()
    {
        itemSpawner.CallItemSpawn(this.gameObject, gameObject.transform.position);
        photonView.RPC(PunRPCList.DeleteItemBox.ToString(), PhotonTargets.AllViaServer);
    }

	[PunRPC]
	void DeleteItemBox()
	{
		Destroy(this.gameObject);
	}
}

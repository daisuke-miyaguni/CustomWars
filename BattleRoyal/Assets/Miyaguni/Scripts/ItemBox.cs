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

    [SerializeField] int itemSpawnCount = 3;

	void Start()
	{
		photonView = GetComponent<PhotonView>();
        itemSpawner = GameObject.FindWithTag("ItemSpawner").gameObject.GetComponent<ItemSpawner>();
	}
    
    // ボタンでボックスを開ける
    public void OpenOnClick()
    {
        AudioManager.Instance.PlaySE("inserting-key-into-wooden-door-1");
        Vector3 objectPos = this.gameObject.transform.position;
        itemSpawner.CallItemSpawn(this.gameObject, objectPos, itemSpawnCount);
        photonView.RPC(PunRPCList.DeleteItemBox.ToString(), PhotonTargets.AllViaServer);
    }

	[PunRPC]
	void DeleteItemBox()
	{
		Destroy(this.gameObject);
        PlayerUIController playerUIs = GameObject.FindWithTag("PlayerControllerUI").GetComponent<PlayerUIController>();
        playerUIs.openMiyaguniButton.gameObject.SetActive(false);
	}
}

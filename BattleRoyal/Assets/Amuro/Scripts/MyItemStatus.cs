﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyItemStatus : MonoBehaviour
{
    private ItemData itemData;
    private ItemParam param;

    PhotonView myItemPV;

    public void SetMyItemPV(PhotonView pv)
    {
        myItemPV = pv;
    }

    public GameObject getButton;

    public enum Item
    {
        parts1,
        parts2,
        parts3,
        mon,
        ball,
        riyo
    };

    [SerializeField]
    public static bool[] itemFlags = new bool[6];                   //　アイテムを持っているかどうかのフラグ

    void Start()
    {
        getButton = GameObject.Find("PlayerControllerUI").gameObject.transform.Find("getButton").gameObject;
        if (getButton.activeSelf)
        {
            getButton.SetActive(false);

        }
    }



    private void OnTriggerStay(Collider other)
    {
        if (myItemPV.isMine && other.gameObject.tag == "Item")
        {
            param = other.gameObject.GetComponent<ItemParam>();
            var type = param.GetItems();
            if (itemFlags[(int)type])
            {
                return;
            }
            getButton.SetActive(true);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (myItemPV.isMine && other.gameObject.tag == "Item")
        {
            param = null;
            getButton.SetActive(false);
        }
    }

    public void OnGetButton()
    {
        if (myItemPV.isMine)
        {
            myItemPV.RPC("WasgetItem", PhotonTargets.AllViaServer);

            var type = param.GetItems();

            itemFlags[(int)type] = true;
        }
    }

    [PunRPC]
    void WasgetItem()
    {
        Destroy(param.gameObject);
        if (myItemPV.isMine)
        {
            getButton.SetActive(false);
        }
    }



    public bool GetItemFlag(Item item)
    {
        return itemFlags[(int)item];
    }
}

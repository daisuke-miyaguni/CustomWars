using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField]
    private ItemDataBase itemDataBase;

    PhotonView itemSpawnPV;

    GameObject itemBox;
    [SerializeField] GameObject[] items;

    [SerializeField] float itemSpawnPower = 5.0f;


    void Awake()
    {
        itemSpawnPV = GetComponent<PhotonView>();
    }

    public void CallItemSpawn(GameObject callObject, Vector3 spawnPos, int itemNumber)
    {
        switch(callObject.tag)
        {
            case "ItemBox":
                itemSpawnPV.RPC("BoxOpen", PhotonTargets.MasterClient, spawnPos, itemNumber);
                break;

            case "PlayerControllerUI":
                itemSpawnPV.RPC("DropItem", PhotonTargets.MasterClient, spawnPos, itemNumber);
                break;

            //死亡時アイテムドロップ　12/21
            case "Player":
                itemSpawnPV.RPC("DeathDrop", PhotonTargets.MasterClient, callObject);
                break;
                
            default:
                break;
        }
    }

    [PunRPC]
    void BoxOpen(Vector3 boxPos, int count)
    {
        int spawnCount = count;
        GameObject[] randItem = items.OrderBy(i => Guid.NewGuid()).ToArray();

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject item = PhotonNetwork.InstantiateSceneObject
            (
                randItem[i].name,
                boxPos,
                Quaternion.Euler(new Vector3(0, 1, 0) * UnityEngine.Random.Range(0f, 180f)),
                0,
                null
            );
            Rigidbody itemRb = item.GetComponent<Rigidbody>();
            itemRb.AddForce
            (
                UnityEngine.Random.Range(-itemSpawnPower, itemSpawnPower),
                itemSpawnPower,
                UnityEngine.Random.Range(0.5f, itemSpawnPower),
                ForceMode.VelocityChange
            );
        }
    }

    [PunRPC]
    void DropItem(Vector3 playerPos, int spawnItemNum)
    {
        GameObject item = items[spawnItemNum];

        item = PhotonNetwork.InstantiateSceneObject
        (
            item.name,
            playerPos,
            Quaternion.Euler(new Vector3(0, 1, 0) * UnityEngine.Random.Range(0f, 180f)),
            0,
            null
        );

        Rigidbody itemRb = item.GetComponent<Rigidbody>();

        itemRb.AddForce
        (
            UnityEngine.Random.Range(-itemSpawnPower, itemSpawnPower),
            itemSpawnPower,
            UnityEngine.Random.Range(0.5f, itemSpawnPower),
            ForceMode.VelocityChange
        );
    }

    //死亡時アイテムドロップ　12/21
    [PunRPC]
    void DeathDrop(GameObject myPlayer)
    {   
        Debug.Log("ok");
        MyItemStatus myItemStatus = myPlayer.gameObject.GetComponent<MyItemStatus>();
        itemDataBase = itemDataBase.GetComponent<ItemDataBase>();
        
        foreach (var item in itemDataBase.GetItemData())
        {
            if (myItemStatus.GetItemFlag(item.GetItemType()))
            {
                GameObject instantItem = items[item.GetItemId()];

                for (int i = 0; i < myItemStatus.GetItemCount(item.GetItemId()); i++)
                {
                   instantItem = PhotonNetwork.InstantiateSceneObject
                    (
                        instantItem.name,
                        myPlayer.transform.position,
                        Quaternion.Euler(new Vector3(0, 1, 0) * UnityEngine.Random.Range(0f, 180f)),
                        0,
                        null
                    );

                    Rigidbody itemRb = instantItem.GetComponent<Rigidbody>();

                    itemRb.AddForce
                    (
                        UnityEngine.Random.Range(-itemSpawnPower, itemSpawnPower),
                        itemSpawnPower,
                        UnityEngine.Random.Range(0.5f, itemSpawnPower),
                        ForceMode.VelocityChange
                    );
                }
            }
        }
    }
}

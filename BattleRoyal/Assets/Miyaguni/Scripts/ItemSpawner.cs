using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    PhotonView itemSpawnPV;
    [SerializeField] GameObject[] items;

    [SerializeField] float itemSpawnPower;

    private const int itemCount = 3;

    GameObject itemBox;


    void Awake()
    {
        itemSpawnPV = GetComponent<PhotonView>();
    }

    public void CallItemSpawn(GameObject callObject, Vector3 spawnPos)
    {
        if (callObject.tag == "ItemBox")
        {
            itemSpawnPV.RPC("BoxOpen", PhotonTargets.MasterClient, itemCount, spawnPos);
        }
        else
        {
            itemSpawnPV.RPC("DropItem", PhotonTargets.MasterClient, callObject, spawnPos);
        }
    }

    [PunRPC]
    void BoxOpen(int maxItemSpawnCount, Vector3 boxPos)
    {
        GameObject[] randItem = items.OrderBy(i => Guid.NewGuid()).ToArray();

        for (int i = 0; i < maxItemSpawnCount; i++)
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
    void DropItem(GameObject dropItem, Vector3 playerPos)
    {
        GameObject item = PhotonNetwork.InstantiateSceneObject
        (
            dropItem.name,
            playerPos,
            Quaternion.Euler(new Vector3(0, 1, 0) * UnityEngine.Random.Range(0f, 180f)),
            0,
            null
        );
        Rigidbody itemRb = dropItem.GetComponent<Rigidbody>();
        itemRb.AddForce
        (
            UnityEngine.Random.Range(-itemSpawnPower, itemSpawnPower),
            itemSpawnPower,
            UnityEngine.Random.Range(0.5f, itemSpawnPower),
            ForceMode.VelocityChange
        );
    }
}

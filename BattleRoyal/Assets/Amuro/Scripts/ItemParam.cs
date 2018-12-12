﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParam : MonoBehaviour
{
    private ItemData itemData;

    private ItemDataBase itemDataBase;

    public int itemNum;

    private MyItemStatus.Item items;

    Rigidbody rb;
    PhotonView ipPV;
    PhotonTransformView ipPTV;

    // Use this for initialization
    void Start()
    {
        switch (itemNum)
        {
            case 0:
                items = MyItemStatus.Item.parts1;
                break;

            case 1:
                items = MyItemStatus.Item.parts2;
                break;

            case 2:
                items = MyItemStatus.Item.parts3;
                break;

            case 3:
                items = MyItemStatus.Item.mon;
                break;

            case 4:
                items = MyItemStatus.Item.ball;
                break;

            case 5:
                items = MyItemStatus.Item.riyo;
                break;
        }

        rb = GetComponent<Rigidbody>();
        ipPV = GetComponent<PhotonView>();
        ipPTV = GetComponent<PhotonTransformView>();
    }

    public MyItemStatus.Item GetItems()
    {
        return items;
    }

    // Update is called once per frame
    void Update()
    {
        if (ipPV.isMine)
        {
            Vector3 velocity = rb.velocity;
            ipPTV.SetSynchronizedValues(speed: velocity, turnSpeed: 0);
        }
    }
}
